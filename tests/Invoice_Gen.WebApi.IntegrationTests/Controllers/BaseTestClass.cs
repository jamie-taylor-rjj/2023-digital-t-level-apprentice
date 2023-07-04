namespace Invoice_Gen.WebApi.IntegrationTests.Controllers;

[ExcludeFromCodeCoverage]
public class BaseTestClass : IClassFixture<CustomWebApplicationFactory>
{
    internal readonly HttpClient _client;

    public BaseTestClass(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

}
