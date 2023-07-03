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

    public IQueryable<Invoice> GetAsQueryable()
    {
        using (_logger.BeginScope("{RepositoryName} - getting IQueryable of {RecordName}",
                   nameof(InvoiceRepository), nameof(Invoice)))
        {
            return _dbContext.Invoices.AsQueryable();
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

    public async Task Delete(int invoiceId)
    {
        using (_logger.BeginScope("{RepositoryName} - deleting {RecordName} with ID of {RecordId}",
                   nameof(InvoiceRepository), nameof(invoiceId), invoiceId))
        {
            _logger.LogInformation("Ensuring that a record with matching ID exists");
            var entity = _dbContext.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
            if (entity != null)
            {
                _logger.LogInformation("Found matching record, deleting now.");
                _dbContext.Invoices.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
