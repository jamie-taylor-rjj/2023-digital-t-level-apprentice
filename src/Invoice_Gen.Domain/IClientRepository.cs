using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.Domain;

public interface IClientRepository
{
    List<Client> GetAll();
}
