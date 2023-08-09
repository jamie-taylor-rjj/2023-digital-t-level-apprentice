namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

public class ClientServiceTests
{
    private readonly int _clientId;
    private readonly string _clientName;
    private readonly string _clientAddress;
    private readonly string _contactName;
    private readonly string _contactEmail;

    private readonly IMapper<ClientViewModel, Client> _mockedClientNameViewModelMapper;

    public ClientServiceTests()
    {
        var rng = new Random();
        _clientId = rng.Next(1, 200);
        _clientName = Guid.NewGuid().ToString();
        _clientAddress = Guid.NewGuid().ToString();
        _contactName = Guid.NewGuid().ToString();
        _contactEmail = Guid.NewGuid().ToString();

        _mockedClientNameViewModelMapper = Substitute.For<IMapper<ClientViewModel, Client>>();
    }

    [Fact]
    public void Given_ValidInput_GetPage_Returns_Valid_PagedResponseOfClientViewModel()
    {
        // Arrange
        const int numberOfClients = 200;
        const int pageNumber = 1;
        const int pageSize = 10;
        var clientsForMock = ClientHelpers.GenerateRandomListOfClients(200);
        var mockedRepository = Substitute.For<IClientRepository>();
        mockedRepository.GetAsQueryable().ReturnsForAnyArgs(clientsForMock.AsQueryable());
        var mockedLogger = Substitute.For<ILogger<ClientPager>>();

        _mockedClientNameViewModelMapper.Convert(new Client()).ReturnsForAnyArgs(new ClientViewModel());

        var sut = new ClientPager(_mockedClientNameViewModelMapper, mockedRepository, mockedLogger);

        // Act
        var result = sut.GetPage(pageNumber);

        // Assert
        Assert.IsAssignableFrom<PagedResponse<ClientViewModel>>(result);
        Assert.Equal(pageSize, result.Data.Count);
        Assert.Equal(pageNumber, result.PageNumber);
        Assert.Equal(numberOfClients / pageSize, result.TotalPages);
        Assert.Equal(numberOfClients, result.TotalRecords);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public void Given_PageNumberLessThanOne_GetPage_Returns_FirstPageOf_PagedResponseOfClientViewModel(int pageNumber)
    {
        // Arrange
        const int numberOfClients = 200;
        const int pageSize = 10;
        var clientsForMock = ClientHelpers.GenerateRandomListOfClients(200);
        var mockedRepository = Substitute.For<IClientRepository>();
        mockedRepository.GetAsQueryable().ReturnsForAnyArgs(clientsForMock.AsQueryable());
        var mockedLogger = Substitute.For<ILogger<ClientPager>>();

        _mockedClientNameViewModelMapper.Convert(new Client()).ReturnsForAnyArgs(new ClientViewModel());

        var sut = new ClientPager(_mockedClientNameViewModelMapper, mockedRepository, mockedLogger);

        // Act
        var result = sut.GetPage(pageNumber);

        // Assert
        Assert.IsAssignableFrom<PagedResponse<ClientViewModel>>(result);
        Assert.Equal(pageSize, result.Data.Count);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(numberOfClients / pageSize, result.TotalPages);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Fact]
    public async Task Given_Valid_ClientId_DeleteClient_DoesntRaiseException()
    {
        // Arrange
        var client = new Client
        {
            ClientId = 1,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IClientRepository>();
        mockedRepository.GetAll().ReturnsForAnyArgs(clientsForMock);
        mockedRepository.Delete(0).ReturnsForAnyArgs(Task.FromResult(0));
        var mockedLogger = Substitute.For<ILogger<ClientPager>>();

        var sut = new ClientPager(_mockedClientNameViewModelMapper, mockedRepository, mockedLogger);

        // act
        var exception = await Record.ExceptionAsync(() => sut.DeleteClient(1));

        // Assert
        Assert.Null(exception);
    }
}
