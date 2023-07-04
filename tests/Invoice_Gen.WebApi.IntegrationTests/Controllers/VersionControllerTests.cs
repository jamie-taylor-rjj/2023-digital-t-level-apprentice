namespace Invoice_Gen.WebApi.IntegrationTests.Controllers;

public class VersionControllerTests : BaseTestClass
{
    public VersionControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Get_Returns_VersionResponse()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("");

        // Assert
        response.EnsureSuccessStatusCode();

        var versionData = await response.Content.ReadFromJsonAsync<VersionResponse>();
        Assert.NotNull(versionData);
        Assert.False(string.IsNullOrWhiteSpace(versionData.VersionNumber));
        Assert.False(string.IsNullOrWhiteSpace(versionData.AppName));
    }
}
