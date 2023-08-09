namespace Invoice_Gen.WebApi.UnitTests.RepoTests;

public class ClientRepoTests
{
    private readonly DbContextOptions<InvoiceGenDbContext> _contextOptions;

    public ClientRepoTests()
    {
        _contextOptions = new DbContextOptionsBuilder<InvoiceGenDbContext>()
            .UseInMemoryDatabase("Invoice_Gen.WebApi.UnitTests.RepoTests.ClientRepoTests.InMemoryContext")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
    }

    [Fact]
    public async Task GetAll_Returns_ListOfClientInstances()
    {
        // arrange
        var clientList = ClientHelpers.GenerateRandomListOfClients(100);

        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Clients.AddRangeAsync(clientList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<ClientRepository>>();

        var sut = new ClientRepository(context, mockedLogger);

        // act
        var response = sut.GetAll();

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<List<Client>>(response);
        Assert.NotEmpty(response);
    }

    [Fact]
    public async Task GetAsQueryable_Returns_ListOfClientInstances_AsQueryable()
    {
        // arrange
        var clientList = ClientHelpers.GenerateRandomListOfClients(100);
        
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Clients.AddRangeAsync(clientList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<ClientRepository>>();

        var sut = new ClientRepository(context, mockedLogger);

        // act
        var response = sut.GetAsQueryable();

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<IQueryable<Client>>(response);
    }

    [Fact]
    public async Task Add_AddsInstance_ToRepo()
    {
        // arrange
        var clientList = new List<Client>
        {
            new()
            {
                ClientName = "Testy McTestFace",
                ContactName = "Tester McContactFace",
                ClientAddress = "Boaty McBoatFace",
                ContactEmail = "mctestface.testy@testl.library"
            }
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Clients.AddRangeAsync(clientList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<ClientRepository>>();

        var sut = new ClientRepository(context, mockedLogger);

        var clientToAdd = new Client
        {
            ClientName = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
            ClientAddress = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
        };

        // act
        var response = await sut.Add(clientToAdd);

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<Client>(response);

        Assert.Equal(clientToAdd.ClientName, response.ClientName);
        Assert.Equal(clientToAdd.ContactEmail, response.ContactEmail);
        Assert.Equal(clientToAdd.ClientAddress, response.ClientAddress);
        Assert.Equal(clientToAdd.ContactName, response.ContactName);
    }

    [Fact]
    public async Task Delete_RemovesInstance_FromRepo()
    {
        // arrange
        var clientList = new List<Client>
        {
            new()
            {
                ClientId = 1,
                ClientName = "Testy McTestFace",
                ContactName = "Tester McContactFace",
                ClientAddress = "Boaty McBoatFace",
                ContactEmail = "mctestface.testy@testl.library"
            },
            new()
            {
                ClientId = 2,
                ClientName = "Testy McTestFace",
                ContactName = "Tester McContactFace",
                ClientAddress = "Boaty McBoatFace",
                ContactEmail = "mctestface.testy@testl.library"
            },
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Clients.AddRangeAsync(clientList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<ClientRepository>>();

        var sut = new ClientRepository(context, mockedLogger);

        // act
        await sut.Delete(2);
        var listAfterDelete = sut.GetAll();

        // asset
        Assert.NotNull(listAfterDelete);
        Assert.IsAssignableFrom<List<Client>>(listAfterDelete);
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
        var clientList = new List<Client>
        {
            new()
            {
                ClientId = 1,
                ClientName = "Testy McTestFace",
                ContactName = "Tester McContactFace",
                ClientAddress = "Boaty McBoatFace",
                ContactEmail = "mctestface.testy@testl.library"
            },
            new()
            {
                ClientId = 2,
                ClientName = "Testy McTestFace",
                ContactName = "Tester McContactFace",
                ClientAddress = "Boaty McBoatFace",
                ContactEmail = "mctestface.testy@testl.library"
            },
        };
        await using var context = new InvoiceGenDbContext(_contextOptions);
        await DeleteAll(context);
        await context.Clients.AddRangeAsync(clientList);
        await context.SaveChangesAsync();
        
        var mockedLogger = Substitute.For<ILogger<ClientRepository>>();

        var sut = new ClientRepository(context, mockedLogger);

        // act
        await sut.Delete(targetClientId);
        var listAfterDelete = sut.GetAll();

        // asset
        Assert.NotNull(listAfterDelete);
        Assert.IsAssignableFrom<List<Client>>(listAfterDelete);
        Assert.False(listAfterDelete.Count == 1);
    }

    private async Task DeleteAll(InvoiceGenDbContext context)
    {
        foreach (var client in context.Clients)
        {
            context.Clients.Remove(client);
        }

        await context.SaveChangesAsync();
    }
}
