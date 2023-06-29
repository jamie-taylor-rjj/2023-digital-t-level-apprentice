namespace Invoice_Gen.WebApi.Services;

public interface IInvoiceService
{
    List<InvoiceViewModel> GetInvoices();
    InvoiceViewModel? GetById(int id);
    Task<int> CreateNewInvoice(InvoiceCreateModel newInvoice);
    Task DeleteInvoice(int invoiceId);
}
