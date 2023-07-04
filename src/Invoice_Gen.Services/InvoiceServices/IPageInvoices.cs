namespace InvoiceGen.Services.InvoiceServices;

public interface IPageInvoices
{
    PagedResponse<InvoiceViewModel> GetPage(int pageNumber, int pageSize = 10);
}
