namespace Invoice_Gen.WebApi.UnitTests.RepoTests;

public class LineItemRepoTests
{
    private readonly DbContextOptions<InvoiceGenDbContext> _contextOptions;

    public LineItemRepoTests()
    {
        _contextOptions = new DbContextOptionsBuilder<InvoiceGenDbContext>()
            .UseInMemoryDatabase("Invoice_Gen.WebApi.UnitTests.RepoTests.LineItemRepoTests.InMemoryContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
    }
    
    [Fact]
    public async Task GetAll_Returns_ListOfLineItemInstances()
    {
        // arrange
        var lineItemList = new List<LineItem>
        {
            new()
            {
                InvoiceId = 1,
                Cost = 100,
                Description = Guid.NewGuid().ToString(),
                Quantity = 1
            }
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.LineItems.AddRangeAsync(lineItemList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<LineItemRepository>>();

        var sut = new LineItemRepository(mockedLogger, context);

        // act
        var response = sut.GetAll();

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<List<LineItem>>(response);
        Assert.NotEmpty(response);
    }
    
    private async Task DeleteAll(InvoiceGenDbContext context)
    {
        foreach (var lineItems in context.LineItems)
        {
            context.LineItems.Remove(lineItems);
        }

        await context.SaveChangesAsync();
    }
}
