using Invoice_Gen.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice_Gen.WebApi.IntegrationTests;

[ExcludeFromCodeCoverage]
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(InvoiceGenDbContext));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }


            services.AddDbContext<InvoiceGenDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryEmployeeTest");
            });
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<InvoiceGenDbContext>();
            appContext.Database.EnsureCreated();
        });
    }
}
