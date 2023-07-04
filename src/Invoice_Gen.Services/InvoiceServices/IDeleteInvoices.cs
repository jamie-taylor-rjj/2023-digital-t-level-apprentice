namespace InvoiceGen.Services.InvoiceServices;

public interface IDeleteInvoices
{
    Task DeleteInvoice(int invoiceId);

}
