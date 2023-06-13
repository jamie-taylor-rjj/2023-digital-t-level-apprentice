using Invoice_Gen.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.Domain;

public class ClientRepository : IClientRepository
{
    private readonly IDbContext _dbContext;
    private readonly ILogger<ClientRepository> _logger;

    public ClientRepository(IDbContext dbContext, ILogger<ClientRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public List<Client> GetAll()
    {
        using (_logger.BeginScope("{RepositoryName} - getting all {RecordName} records",
                   nameof(ClientRepository), nameof(Client)))
        {
            return _dbContext.Clients.ToList();
        }
    }

    public async Task<Client> Add(Client client)
    {
        using (_logger.BeginScope("{RepositoryName} - adding new {RecordName}",
                   nameof(ClientRepository), nameof(Client)))
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }
    }

    public async Task Delete(int clientId)
    {
        using (_logger.BeginScope("{RepositoryName} - deleting {RecordName} with ID of {RecordId}",
                   nameof(ClientRepository), nameof(Client), clientId))
        {
            _logger.LogInformation("Ensuring that a record with matching ID exists");
            var entity = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
            if (entity != null)
            {
                _logger.LogInformation("Found matching record, deleting now.");
                _dbContext.Clients.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
