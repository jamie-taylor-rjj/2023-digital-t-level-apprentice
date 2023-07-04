using Invoice_Gen.Domain.Models;
using InvoiceGen.Services;
using InvoiceGen.Services.ClientServices;
using InvoiceGen.Services.InvoiceServices;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Gen.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMappers(this IServiceCollection services)
    {
        services
            .AddTransient<IMapper<ClientViewModel, Client>, ClientNameViewModelMapper>()
            .AddTransient<IMapper<InvoiceViewModel, Invoice>, InvoiceViewModelMapper>()
            .AddTransient<IMapper<InvoiceCreateModel, Invoice>, InvoiceCreateModelMapper>()
            .AddTransient<IMapper<LineItemViewModel, LineItem>, LineItemViewModelMapper>();
    }

    public static void AddRepos(this IServiceCollection services)
    {
        services
            .AddTransient<IClientRepository, ClientRepository>()
            .AddTransient<IInvoiceRepository, InvoiceRepository>()
            .AddTransient<ILineItemRepository, LineItemRepository>();
    }

    public static void AddDbContext(this IServiceCollection services, string connectionString)
    {
        services
            .AddTransient<IDbContext, InvoiceGenDbContext>()
            .AddDbContext<InvoiceGenDbContext>(opt => opt.UseSqlite(connectionString));
    }

    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IGetClients, ClientGetter>()
            .AddTransient<ICreateClients, ClientCreator>()
            .AddTransient<IDeleteClients, ClientDeleter>()
            .AddTransient<IPageClients, ClientPager>();
    }

    public static IServiceCollection AddInvoiceServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IGetInvoices, InvoiceGetter>()
            .AddTransient<ICreateInvoices, InvoiceCreator>()
            .AddTransient<IDeleteInvoices, InvoiceDeleter>()
            .AddTransient<IPageInvoices, InvoicePager>();
    }

    public static void AddLineItemServices(this IServiceCollection services)
    {
        services.AddTransient<IGetLineItems, LineItemGetter>();
    }
}
