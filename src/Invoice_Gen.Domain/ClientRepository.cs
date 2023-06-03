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
}
