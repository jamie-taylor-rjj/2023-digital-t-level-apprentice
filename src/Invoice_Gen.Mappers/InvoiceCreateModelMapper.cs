using Invoice_Gen.Domain.Models;
using Invoice_Gen.ViewModels;

namespace Invoice_Gen.Mappers;

public class InvoiceCreateModelMapper : IMapper<InvoiceCreateModel, Invoice>
{
    public Invoice Convert(InvoiceCreateModel source)
    {
        var lineItems = source.LineItems.Any()
            ? source.LineItems.Select(sl => new LineItem
            {
                Cost = sl.Cost,
                Description = sl.Description,
                Quantity = sl.Quantity
            }
            )
            : new List<LineItem>();

        return new Invoice
        {
            ClientId = source.ClientId,
            DueDate = source.DueDate,
            IssueDate = source.IssueDate,
            VatRate = source.VatRate,
            LineItems = lineItems.ToList()
        };

    }

    public InvoiceCreateModel Convert(Invoice destination)
    {
        throw new NotImplementedException();
    }
}
