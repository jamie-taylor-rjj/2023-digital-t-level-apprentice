using System.Text.Json;

namespace Invoice_Gen.WebApi.IntegrationTests;

[ExcludeFromCodeCoverage]
public class HelloEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HelloEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact] public async Task Hello_Returns_OK()
    {
        // arrange
        var client = _factory.CreateClient();
        
        // act
        var response = await client.GetAsync("Hello");

        // assert
        response.EnsureSuccessStatusCode();
    }
}
