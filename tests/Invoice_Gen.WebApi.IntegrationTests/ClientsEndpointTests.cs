using System.Net;
using System.Text.Json;
using Invoice_Gen.ViewModels;

namespace Invoice_Gen.WebApi.IntegrationTests;

[ExcludeFromCodeCoverage]
public class ClientsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ClientsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Clients_Returns_ListOf_ClientInstances()
    {
        // arrange
        var client = _factory.CreateClient();
        
        // act
        var response = await client.GetAsync("Clients");

        // assert
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var listOfClients = JsonSerializer.Deserialize<List<ClientNameViewModel>>(responseContent);

        Assert.NotNull(listOfClients);
        Assert.IsAssignableFrom<List<ClientNameViewModel>>(listOfClients);
    }
    
    [Fact]
    public async Task ClientById_With_ValidId_Returns_Client()
    {
        // arrange
        var client = _factory.CreateClient();
        
        // act
        var response = await client.GetAsync("Clients/1");

        // assert
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var clientDetails = JsonSerializer.Deserialize<ClientNameViewModel>(responseContent);

        Assert.NotNull(clientDetails);
        Assert.IsAssignableFrom<ClientNameViewModel>(clientDetails);
    }
    
    [Fact]
    public async Task ClientById_With_InvalidId_Returns_NotFound()
    {
        // arrange
        var client = _factory.CreateClient();
        
        // act
        var response = await client.GetAsync("Clients/-1");

        // assert
        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
