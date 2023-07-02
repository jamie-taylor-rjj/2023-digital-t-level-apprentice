namespace Invoice_Gen.WebApi.UnitTests.MapperTests;

public class ClientNameViewModelMapperTests
{
    private readonly IMapper<ClientViewModel, Client> _mapper;
    private readonly Random _rng;

    public ClientNameViewModelMapperTests()
    {
        _rng = new Random();
        _mapper = new ClientNameViewModelMapper();
    }

    [Fact]
    public void Given_Client_Can_MapTo_ClientNameViewModel()
    {
        // Arrange
        var client = new Client
        {
            ClientId = _rng.Next(1, 200),
            ClientName = Guid.NewGuid().ToString(),
            ClientAddress = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
        };

        // Act
        var clientViewModel = _mapper.Convert(client);

        // Assert
        Assert.NotNull(clientViewModel);
        Assert.IsAssignableFrom<ClientViewModel>(clientViewModel);
        Assert.Equal(client.ClientId, clientViewModel.ClientId);
        Assert.Equal(client.ClientName, clientViewModel.ClientName);
        Assert.Equal(client.ClientAddress, clientViewModel.ClientAddress);
        Assert.Equal(client.ContactEmail, clientViewModel.ContactEmail);
        Assert.Equal(client.ContactName, clientViewModel.ContactName);
    }

    [Fact]
    public void Given_ClientViewModel_Cannot_MapTo_Client()
    {
        // Arrange
        var clientViewModel = new ClientViewModel
        {
            ClientName = Guid.NewGuid().ToString(),
            ClientId = _rng.Next(1, 200)
        };

        // Act
        var exception = Record.Exception(() => _mapper.Convert(clientViewModel));

        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<NotImplementedException>(exception);
    }
}
