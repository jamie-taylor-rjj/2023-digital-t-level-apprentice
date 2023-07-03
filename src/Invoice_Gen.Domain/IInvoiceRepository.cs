using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.Domain;

public interface IInvoiceRepository
{
    List<Invoice> GetAll();
    IQueryable<Invoice> GetAsQueryable();
    Task<Invoice> Add(Invoice invoice);
    Task Delete(int invoiceId);
}
