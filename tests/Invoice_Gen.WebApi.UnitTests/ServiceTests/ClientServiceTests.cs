using Invoice_Gen.ViewModels;
using Invoice_Gen.WebApi.Repositories;
using Invoice_Gen.WebApi.Services;

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

    private readonly Mock<IMapper<ClientNameViewModel, Client>> _mockedClientNameViewModelMapper;

    public ClientServiceTests()
    {
        _rng = new Random();
        _clientId = _rng.Next(1, 200);
        _clientName = Guid.NewGuid().ToString();
        _clientAddress = Guid.NewGuid().ToString();
        _contactName = Guid.NewGuid().ToString();
        _contactEmail = Guid.NewGuid().ToString();

        _mockedClientNameViewModelMapper = new Mock<IMapper<ClientNameViewModel, Client>>();
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

        var expectedOutput = new ClientNameViewModel
        {
            ClientId = _clientId,
            ClientName = _clientName,
        };

        _mockedClientNameViewModelMapper.Setup(x => x.Convert(client)).Returns(expectedOutput);

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object,
            mockedRepository.Object);

        // Act
        var result = sut.GetClients();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<ClientNameViewModel>>(result);

        Assert.Equal(_clientId, result.FirstOrDefault()?.ClientId);
        Assert.Equal(_clientName, result.FirstOrDefault()?.ClientName);
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

        var expectedOutput = new ClientNameViewModel()
        {
            ClientId = _clientId,
            ClientName = _clientName,
        };

        _mockedClientNameViewModelMapper.Setup(x => x.Convert(client)).Returns(expectedOutput);

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object,
            mockedRepository.Object);

        // Act
        var result = sut.GetById(_clientId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ClientNameViewModel>(result);

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

        var sut = new ClientService(_mockedClientNameViewModelMapper.Object, mockedRepository.Object);

        // Act
        var result = sut.GetById(_rng.Next(200, 300));

        // Assert
        Assert.Null(result);
    }

}
