namespace Invoice_Gen.WebApi.IntegrationTests;

[ExcludeFromCodeCoverage]
public class VersionEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public VersionEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Version_Returns_OK()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync("");

        // assert
        response.EnsureSuccessStatusCode();
    }
}
