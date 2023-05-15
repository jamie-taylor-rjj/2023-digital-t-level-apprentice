using Invoice_Gen.ViewModels;

namespace Invoice_Gen.WebApi.UnitTests.MapperTests;

[ExcludeFromCodeCoverage]
public class ClientNameViewModelMapperTests
{
    private readonly IMapper<ClientNameViewModel, Client> _mapper;
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
            ClientAddress = Guid.NewGuid().ToString(),
            ClientName = Guid.NewGuid().ToString(),
            ContactEmail = Guid.NewGuid().ToString(),
            ContactName = Guid.NewGuid().ToString(),
            ClientId = _rng.Next(1, 200)
        };

        // Act
        var clientViewModel = _mapper.Convert(client);

        // Assert
        Assert.NotNull(clientViewModel); Assert.IsAssignableFrom<ClientNameViewModel>(clientViewModel);
        Assert.Equal(client.ClientName, clientViewModel.ClientName);
        Assert.Equal(client.ClientId, clientViewModel.ClientId);
    }

    [Fact]
    public void Given_ClientViewModel_Cannot_MapTo_Client()
    {
        // Arrange
        var clientViewModel = new ClientNameViewModel()
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
