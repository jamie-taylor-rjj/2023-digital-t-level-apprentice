using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.Domain;

public interface IClientRepository
{
    List<Client> GetAll();
    Task<Client> Add(Client client);
    Task Delete(int clientId);
}
