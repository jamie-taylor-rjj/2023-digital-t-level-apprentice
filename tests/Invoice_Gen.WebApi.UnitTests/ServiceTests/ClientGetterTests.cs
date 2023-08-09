namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

public class ClientGetterTests
{
    private readonly Random _rng;
    private readonly int _clientId;
    private readonly string _clientName;
    private readonly string _clientAddress;
    private readonly string _contactName;
    private readonly string _contactEmail;

    private readonly IMapper<ClientViewModel, Client> _mockedClientNameViewModelMapper;

    public ClientGetterTests()
    {
        _rng = new Random();
        _clientId = _rng.Next(1, 200);
        _clientName = Guid.NewGuid().ToString();
        _clientAddress = Guid.NewGuid().ToString();
        _contactName = Guid.NewGuid().ToString();
        _contactEmail = Guid.NewGuid().ToString();

        _mockedClientNameViewModelMapper = Substitute.For<IMapper<ClientViewModel, Client>>();
    }

    [Fact]
    public void Given_AtLeast_One_Client_GetAll_Should_Return_At_Least_One_ClientViewModel()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };

        var mockedRepository = Substitute.For<IClientRepository>();
        mockedRepository.GetAll().ReturnsForAnyArgs(clientsForMock);
        var mockedLogger = Substitute.For<ILogger<ClientGetter>>();

        var expectedOutput = new ClientViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };

        _mockedClientNameViewModelMapper.Convert(client).ReturnsForAnyArgs(expectedOutput);

        var sut = new ClientGetter(mockedLogger, mockedRepository,
            _mockedClientNameViewModelMapper);

        // Act
        var result = sut.GetClients();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<ClientViewModel>>(result);

        Assert.Equal(_clientId, result.FirstOrDefault()?.ClientId);
        Assert.Equal(_clientName, result.FirstOrDefault()?.ClientName);
        Assert.Equal(_clientAddress, result.FirstOrDefault()?.ClientAddress);
        Assert.Equal(_contactName, result.FirstOrDefault()?.ContactName);
        Assert.Equal(_contactEmail, result.FirstOrDefault()?.ContactEmail);
    }

    [Fact]
    public void Given_A_Valid_ClientId_GetById_Should_Return_The_Matching_ClientViewModel()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IClientRepository>();
        mockedRepository.GetAsQueryable().ReturnsForAnyArgs(clientsForMock.AsQueryable());
        var mockedLogger = Substitute.For<ILogger<ClientGetter>>();

        var expectedOutput = new ClientViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
        };

        _mockedClientNameViewModelMapper.Convert(client).ReturnsForAnyArgs(expectedOutput);

        var sut = new ClientGetter(mockedLogger, mockedRepository,
            _mockedClientNameViewModelMapper);

        // Act
        var result = sut.GetById(_clientId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ClientViewModel>(result);

        Assert.Equal(_clientId, result.ClientId);
        Assert.Equal(_clientName, result.ClientName);
    }

    [Fact]
    public void Given_An_Invalid_ClientId_GetById_Should_Return_Null()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = Substitute.For<IClientRepository>();
        mockedRepository.GetAll().ReturnsForAnyArgs(clientsForMock);
        var mockedLogger = Substitute.For<ILogger<ClientGetter>>();

        var sut = new ClientGetter(mockedLogger, mockedRepository, _mockedClientNameViewModelMapper);

        // Act
        var result = sut.GetById(_rng.Next(200, 300));

        // Assert
        Assert.Null(result);
    }
}
