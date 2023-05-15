using Invoice_Gen.WebApi.Services;

namespace Invoice_Gen.WebApi.IntegrationTests;

/// <summary>
/// Created ahead of when we'll need to inject some custom services - like
/// a mocked ClientService
/// </summary>
/// <typeparam name="TProgram"></typeparam>
[ExcludeFromCodeCoverage]
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace the ClientService with one we can mock, if we want
            services.AddTransient<IClientService, ClientService>();
        });

        builder.UseEnvironment("Development");
    }
}
