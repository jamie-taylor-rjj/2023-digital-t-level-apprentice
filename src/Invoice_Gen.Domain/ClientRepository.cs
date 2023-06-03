using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.Domain;

public class ClientRepository : IClientRepository
{
    private readonly InvoiceGenDbContext _dbContext;

    public ClientRepository(InvoiceGenDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Client> GetAll()
    {
        return _dbContext.Clients.ToList();
    }

    public async Task<Client> Add(Client client)
    {
        _dbContext.Clients.Add(client);
        await _dbContext.SaveChangesAsync();
        return client;
    }

    public async Task Delete(int clientId)
    {
        var entity = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
        if (entity != null)
        {
            _dbContext.Clients.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
