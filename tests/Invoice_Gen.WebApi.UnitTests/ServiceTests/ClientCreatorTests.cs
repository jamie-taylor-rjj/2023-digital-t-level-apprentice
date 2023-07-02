using InvoiceGen.Services.ClientServices;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

public class ClientCreatorTests
{
    private readonly int _clientId;
    private readonly string _clientName;
    private readonly string _clientAddress;
    private readonly string _contactName;
    private readonly string _contactEmail;

    public ClientCreatorTests()
    {
        var rng = new Random();
        _clientId = rng.Next(1, 200);
        _clientName = Guid.NewGuid().ToString();
        _clientAddress = Guid.NewGuid().ToString();
        _contactName = Guid.NewGuid().ToString();
        _contactEmail = Guid.NewGuid().ToString();
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
        var mockedLogger = new Mock<ILogger<ClientCreator>>();

        var sut = new ClientCreator(mockedLogger.Object, mockedRepository.Object);

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
}
