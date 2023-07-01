using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.Domain;

public interface IClientRepository
{
    List<Client> GetAll();
    IQueryable<Client> GetAsQueryable();
    Task<Client> Add(Client client);
    Task Delete(int clientId);
}
