namespace InvoiceGen.Services.InvoiceServices;

public interface ICreateInvoices
{
    Task<int> CreateNewInvoice(InvoiceCreateModel newInvoice);

}
