using System.Text.Json;

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
        var listOfClients = JsonSerializer.Deserialize<List<Client>>(responseContent);

        Assert.NotNull(listOfClients);
        Assert.IsAssignableFrom<List<Client>>(listOfClients);
    }
}
