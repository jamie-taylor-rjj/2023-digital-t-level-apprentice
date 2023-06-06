using System.Reflection;
using ClacksMiddleware.Extensions;
using Invoice_Gen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using OwaspHeaders.Core.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

try
{
    Log.Information("Starting app - registering services");

    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("invoiceConnectionString");

    builder.Services
        .AddTransient<IMapper<ClientNameViewModel, Client>, ClientNameViewModelMapper>()
        .AddTransient(typeof(IClientRepository), typeof(ClientRepository))
        .AddTransient<IDbContext, InvoiceGenDbContext>()
        .AddDbContext<InvoiceGenDbContext>(opt => opt.UseSqlite(connectionString));
    builder.Services.AddTransient<IClientService, ClientService>();

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

// Required for integration tests
public partial class Program { }
