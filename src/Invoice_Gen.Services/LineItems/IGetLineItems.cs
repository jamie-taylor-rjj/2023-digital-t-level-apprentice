namespace InvoiceGen.Services;

public interface IGetLineItems
{
    LineItemViewModel? GetById(int id);
}
