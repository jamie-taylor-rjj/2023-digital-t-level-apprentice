namespace InvoiceGen.Services.InvoiceServices;

public interface IGetInvoices
{
    List<InvoiceViewModel> GetInvoices();
    InvoiceViewModel? GetById(int id);
}
