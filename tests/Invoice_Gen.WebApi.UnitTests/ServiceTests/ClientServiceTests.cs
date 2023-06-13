using Invoice_Gen.WebApi.Services;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

[ExcludeFromCodeCoverage]
public class ClientServiceTests
{
    private readonly Random _rng;
    private readonly int _clientId;
    private readonly string _clientName;
    private readonly string _clientAddress;
    private readonly string _contactName;
    private readonly string _contactEmail;

    private readonly Mock<IMapper<ClientViewModel, Client>> _mockedClientNameViewModelMapper;

    public ClientServiceTests()
    {
        _rng = new Random();
        _clientId = _rng.Next(1, 200);
        _clientName = Guid.NewGuid().ToString();
        _clientAddress = Guid.NewGuid().ToString();
        _contactName = Guid.NewGuid().ToString();
        _contactEmail = Guid.NewGuid().ToString();

        _mockedClientNameViewModelMapper = new Mock<IMapper<ClientViewModel, Client>>();
    }

    [Fact]
    public void Given_Atleast_One_Client_GetAll_Should_Return_At_Least_One_ClientViewModel()
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

        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(clientsForMock);
        var mockedLogger = new Mock<ILogger<ClientService>>();

        var expectedOutput = new ClientViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };

        _mockedClientNameViewModelMapper.Setup(x => x.Convert(client)).Returns(expectedOutput);

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object,
            mockedRepository.Object, mockedLogger.Object);

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
        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(clientsForMock);
        var mockedLogger = new Mock<ILogger<ClientService>>();

        var expectedOutput = new ClientViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
        };

        _mockedClientNameViewModelMapper.Setup(x => x.Convert(client)).Returns(expectedOutput);

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object,
            mockedRepository.Object, mockedLogger.Object);

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
        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(clientsForMock);
        var mockedLogger = new Mock<ILogger<ClientService>>();

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object, mockedRepository.Object, mockedLogger.Object);

        // Act
        var result = sut.GetById(_rng.Next(200, 300));

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Given_Valid_ClientCreationModel_CreateClient_Returns_IdOfNewClient()
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

        var clientToAdd = new Client
        {
            ClientAddress = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
            ClientName = Guid.NewGuid().ToString(),
            ClientId = 7
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(clientsForMock);
        mockedRepository.Setup(x => x.Add(It.IsAny<Client>())).Returns(Task.FromResult(clientToAdd));
        var mockedLogger = new Mock<ILogger<ClientService>>();

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object, mockedRepository.Object, mockedLogger.Object);

        // act
        var response = await sut.CreateNewClient(new ClientCreationModel
        {
            ClientAddress = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
            ClientName = Guid.NewGuid().ToString(),
        });

        // Assert
        Assert.NotEqual(0, response);
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
        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(clientsForMock);
        mockedRepository.Setup(x => x.Delete(It.IsAny<int>()));
        var mockedLogger = new Mock<ILogger<ClientService>>();

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object, mockedRepository.Object, mockedLogger.Object);

        // act
        var exception = await Record.ExceptionAsync(() => sut.DeleteClient(1));

        // Assert
        Assert.Null(exception);
    }
}
