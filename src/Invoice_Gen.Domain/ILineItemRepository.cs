using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.Domain;

public interface ILineItemRepository
{
    List<LineItem> GetAll();
}
