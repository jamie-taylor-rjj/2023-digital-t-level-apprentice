using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ClacksMiddleware.Extensions;
using Invoice_Gen.Domain.Models;
using InvoiceGen.Services;
using InvoiceGen.Services.ClientServices;
using InvoiceGen.Services.InvoiceServices;
using Microsoft.EntityFrameworkCore;
using OwaspHeaders.Core.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

try
{
    Log.Information("Starting app - registering services");

    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("invoiceConnectionString");

    // Add Mappers
    builder.Services
        .AddTransient<IMapper<ClientViewModel, Client>, ClientNameViewModelMapper>()
        .AddTransient<IMapper<InvoiceViewModel, Invoice>, InvoiceViewModelMapper>()
        .AddTransient<IMapper<InvoiceCreateModel, Invoice>, InvoiceCreateModelMapper>()
        .AddTransient<IMapper<LineItemViewModel, LineItem>, LineItemViewModelMapper>();

    // Add repositories
    builder.Services
        .AddTransient<IClientRepository, ClientRepository>()
        .AddTransient<IInvoiceRepository, InvoiceRepository>()
        .AddTransient<ILineItemRepository, LineItemRepository>();

    // Add DB Context
    builder.Services
        .AddTransient<IDbContext, InvoiceGenDbContext>()
        .AddDbContext<InvoiceGenDbContext>(opt => opt.UseSqlite(connectionString));

    builder.Services
        .AddTransient<IGetClients, ClientGetter>()
        .AddTransient<ICreateClients, ClientCreator>()
        .AddTransient<IDeleteClients, ClientDeleter>()
        .AddTransient<IPageClients, ClientPager>()
        .AddTransient<IGetInvoices, InvoiceGetter>()
        .AddTransient<ICreateInvoices, InvoiceCreator>()
        .AddTransient<IDeleteInvoices, InvoiceDeleter>()
        .AddTransient<IPageInvoices, InvoicePager>()
        .AddTransient<IGetLineItems, LineItemGetter>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(o =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    Log.Information("Starting app - building IApplicationBuilder");

    var app = builder.Build();

    app.GnuTerryPratchett();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSecureHeadersMiddleware(
        SecureHeadersMiddlewareExtensions
            .BuildDefaultConfiguration()
    );

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("Stating app - ready to serve requests");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

[ExcludeFromCodeCoverage]
// Needed for integration tests
public partial class Program { }
