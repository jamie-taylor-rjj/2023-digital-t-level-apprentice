using Invoice_Gen.Domain.Models;
using Invoice_Gen.ViewModels;

namespace Invoice_Gen.Mappers;

public class LineItemViewModelMapper : IMapper<LineItemViewModel, LineItem>
{
    public LineItem Convert(LineItemViewModel source)
    {
        throw new NotImplementedException();
    }

    public LineItemViewModel Convert(LineItem destination) =>
        new()
        {
            LineItemId = destination.LineItemId,
            Cost = destination.Cost,
            Description = destination.Description,
            InvoiceId = destination.InvoiceId,
            Quantity = destination.Quantity
        };
}
