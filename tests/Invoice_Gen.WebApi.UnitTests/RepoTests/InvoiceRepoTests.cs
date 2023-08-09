namespace Invoice_Gen.WebApi.UnitTests.RepoTests;

public class InvoiceRepoTests
{
    private readonly DbContextOptions<InvoiceGenDbContext> _contextOptions;

    public InvoiceRepoTests()
    {
        _contextOptions = new DbContextOptionsBuilder<InvoiceGenDbContext>()
            .UseInMemoryDatabase("Invoice_Gen.WebApi.UnitTests.RepoTests.InvoiceRepoTests.InMemoryContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
    }
    
    [Fact]
    public async Task GetAll_Returns_ListOfInvoiceInstances()
    {
        // arrange
        var invoiceList = new List<Invoice>
        {
            new()
            {
                InvoiceId = 1,
                ClientId = 1,
                DueDate = new DateTime(),
                IssueDate = new DateTime(),
                VatRate = 10
            }
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Invoices.AddRangeAsync(invoiceList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<InvoiceRepository>>();

        var sut = new InvoiceRepository(mockedLogger, context);

        // act
        var response = sut.GetAll();

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<List<Invoice>>(response);
        Assert.NotEmpty(response);
    }

    [Fact]
    public async Task GetAsQueryable_Returns_ListOfClientInstances_AsQueryable()
    {
        // arrange
        var invoiceList = InvoiceHelpers.GenerateRandomListOfInvoices(100);
        
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Invoices.AddRangeAsync(invoiceList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<InvoiceRepository>>();

        var sut = new InvoiceRepository(mockedLogger, context);

        // act
        var response = sut.GetAsQueryable();

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<IQueryable<Invoice>>(response);
    }

    [Fact]
    public async Task Add_AddsInstance_ToRepo()
    {
        // arrange
        var invoiceList = new List<Invoice>
        {
            new()
            {
                InvoiceId = 1,
                ClientId = 1,
            }
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Invoices.AddRangeAsync(invoiceList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<InvoiceRepository>>();

        var sut = new InvoiceRepository(mockedLogger, context);

        var invoiceToAdd = new Invoice
        {
            ClientId = 1,
            DueDate = new DateTime(2024, 01, 01),
            IssueDate = new DateTime(2023, 06, 28),
            VatRate = 10
        };

        // act
        var response = await sut.Add(invoiceToAdd);

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<Invoice>(response);

        Assert.Equal(invoiceToAdd.ClientId, response.ClientId);
        Assert.Equal(invoiceToAdd.DueDate, response.DueDate);
        Assert.Equal(invoiceToAdd.IssueDate, response.IssueDate);
        Assert.Equal(invoiceToAdd.VatRate, response.VatRate);
    }

    [Fact]
    public async Task Delete_RemovesInstance_FromRepo()
    {
        // arrange
        var invoiceList = new List<Invoice>
        {
            new()
            {
                InvoiceId = 1,
                ClientId = 1,
                IssueDate = default,
                DueDate = default,
                VatRate = 10,
                LineItems = new List<LineItem>()
            },
            new()
            {
                InvoiceId = 2,
                ClientId = 2,
                IssueDate = default,
                DueDate = default,
                VatRate = 0,
                LineItems = new List<LineItem>(),

            },
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Invoices.AddRangeAsync(invoiceList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<InvoiceRepository>>();

        var sut = new InvoiceRepository(mockedLogger, context);

        // act
        await sut.Delete(2);
        var listAfterDelete = sut.GetAll();

        // asset
        Assert.NotNull(listAfterDelete);
        Assert.IsAssignableFrom<List<Invoice>>(listAfterDelete);
        Assert.Single(listAfterDelete);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(0)]
    [InlineData(Int32.MaxValue)]
    [InlineData(-1)]
    [InlineData(Int32.MinValue)]
    public async Task Delete_DoesntRemoveInstanceWhenInvalidId_ToRepo(int targetClientId)
    {
        // arrange
        var invoiceList = new List<Invoice>
        {
            new()
            {
                InvoiceId = 1,
                ClientId = 1,
                IssueDate = default,
                DueDate = default,
                VatRate = 10,
                LineItems = new List<LineItem>()
            },
            new()
            {
                InvoiceId = 2,
                ClientId = 2,
                IssueDate = default,
                DueDate = default,
                VatRate = 0,
                LineItems = new List<LineItem>(),

            },
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Invoices.AddRangeAsync(invoiceList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<InvoiceRepository>>();

        var sut = new InvoiceRepository(mockedLogger, context);

        // act
        await sut.Delete(targetClientId);
        var listAfterDelete = sut.GetAll();

        // asset
        Assert.NotNull(listAfterDelete);
        Assert.IsAssignableFrom<List<Invoice>>(listAfterDelete);
        Assert.False(listAfterDelete.Count == 1);
    }
    
    private async Task DeleteAll(InvoiceGenDbContext context)
    {
        foreach (var invoice in context.Invoices)
        {
            context.Invoices.Remove(invoice);
        }

        await context.SaveChangesAsync();
    }
}
