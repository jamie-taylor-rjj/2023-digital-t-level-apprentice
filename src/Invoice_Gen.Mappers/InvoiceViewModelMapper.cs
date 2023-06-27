using Invoice_Gen.Domain.Models;
using Invoice_Gen.ViewModels;

namespace Invoice_Gen.Mappers;

public class InvoiceViewModelMapper : IMapper<InvoiceViewModel, Invoice>
{
    private readonly IMapper<LineItemViewModel, LineItem> _lineItemMapper;

    public InvoiceViewModelMapper(IMapper<LineItemViewModel, LineItem> lineItemMapper)
    {
        _lineItemMapper = lineItemMapper;
    }

    public Invoice Convert(InvoiceViewModel source)
    {
        throw new NotImplementedException();
    }

    public InvoiceViewModel Convert(Invoice destination) =>
        new()
        {
            InvoiceId = destination.InvoiceId,
            ClientId = destination.ClientId,
            DueDate = destination.DueDate,
            IssueDate = destination.IssueDate,
            VatRate = destination.VatRate,
            LineItems = destination.LineItems.Select(li => _lineItemMapper.Convert(li)).ToList()
        };
}
