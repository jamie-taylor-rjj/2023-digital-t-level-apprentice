using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ClacksMiddleware.Extensions;
using Invoice_Gen.WebApi.Extensions;
using OwaspHeaders.Core.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

try
{
    Log.Information("Starting app - registering services");

    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddMappers();
    
    builder.Services.AddRepos();
    
    builder.Services
        .AddDbContext(builder.Configuration.GetConnectionString("invoiceConnectionString")!);

    builder.Services
        .AddClientServices()
        .AddInvoiceServices()
        .AddLineItemServices();

    builder.Services.AddControllers();
    
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
