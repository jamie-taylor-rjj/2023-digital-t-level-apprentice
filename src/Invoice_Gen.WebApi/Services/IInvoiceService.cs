namespace Invoice_Gen.WebApi.Services;

public interface IInvoiceService
{
    List<InvoiceViewModel> GetInvoices();
    InvoiceViewModel? GetById(int id);
    PagedResponse<InvoiceViewModel> GetPage(int pageNumber, int pageSize = 10);
    Task<int> CreateNewInvoice(InvoiceCreateModel newInvoice);
    Task DeleteInvoice(int invoiceId);
}
