using Invoice_Gen.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.Domain;

public class ClientRepository : IClientRepository
{
    private readonly InvoiceGenDbContext _dbContext;
    private readonly ILogger<ClientRepository> _logger;

    public ClientRepository(InvoiceGenDbContext dbContext, ILogger<ClientRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public List<Client> GetAll()
    {
        _logger.BeginScope("{ClientRepository} - getting all {Client} records", nameof(ClientRepository), nameof(Client));
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
