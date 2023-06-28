using Invoice_Gen.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.Domain;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly IDbContext _dbContext;
    private readonly ILogger<InvoiceRepository> _logger;

    public InvoiceRepository(ILogger<InvoiceRepository> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public List<Invoice> GetAll()
    {
        using (_logger.BeginScope("{RepositoryName} - getting all {RecordName} records",
                   nameof(InvoiceRepository), nameof(Client)))
        {
            return _dbContext.Invoices.ToList();
        }
    }

    public async Task<Invoice> Add(Invoice invoice)
    {
        using (_logger.BeginScope("{RepositoryName} - adding new {RecordName}",
                   nameof(InvoiceRepository), nameof(Invoice)))
        {
            _dbContext.Invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();
            return invoice;
        }
    }
}
