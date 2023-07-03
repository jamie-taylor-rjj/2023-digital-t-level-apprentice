using Invoice_Gen.ViewModels;

namespace InvoiceGen.Services;

public interface ILineItemService
{
    LineItemViewModel? GetById(int id);
}
