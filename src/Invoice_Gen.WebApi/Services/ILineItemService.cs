namespace Invoice_Gen.WebApi.Services;

public interface ILineItemService
{
    LineItemViewModel? GetById(int id);
}
