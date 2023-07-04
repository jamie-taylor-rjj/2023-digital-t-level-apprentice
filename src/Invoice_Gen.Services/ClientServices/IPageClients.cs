namespace InvoiceGen.Services.ClientServices;

public interface IPageClients
{
    PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10);
}
