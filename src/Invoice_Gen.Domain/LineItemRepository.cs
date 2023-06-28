using Invoice_Gen.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.Domain;

public class LineItemRepository : ILineItemRepository
{
    private readonly IDbContext _dbContext;
    private readonly ILogger<LineItemRepository> _logger;

    public LineItemRepository(ILogger<LineItemRepository> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public List<LineItem> GetAll()
    {
        using (_logger.BeginScope("{RepositoryName} - getting all {RecordName} records",
                   nameof(LineItemRepository), nameof(LineItem)))
        {
            return _dbContext.LineItems.ToList();
        }
    }
}
