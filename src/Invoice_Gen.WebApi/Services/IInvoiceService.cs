namespace Invoice_Gen.WebApi.Services;

public interface IInvoiceService
{
    InvoiceViewModel? GetById(int id);
    Task<int> CreateNewInvoice(InvoiceCreateModel newInvoice);
}
