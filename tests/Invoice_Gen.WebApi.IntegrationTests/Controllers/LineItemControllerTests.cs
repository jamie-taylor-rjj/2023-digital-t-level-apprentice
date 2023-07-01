namespace Invoice_Gen.WebApi.IntegrationTests.Controllers;

[ExcludeFromCodeCoverage]
public class LineItemControllerTests : BaseTestClass
{
    public LineItemControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task GetLineItemById_Given_ValidId_Returns_LineItemViewModel()
    {
        // Arrange
        const int lineItemId = 1;

        // Act
        var response = await _client.GetAsync($"/LineItems/{lineItemId}");

        // Assert
        response.EnsureSuccessStatusCode();

        var lineItem = await response.Content.ReadFromJsonAsync<LineItemViewModel>();
        Assert.NotNull(lineItem);
        Assert.True(lineItem.Description.Equals("Time spent building API (per hour)", StringComparison.OrdinalIgnoreCase));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-32)]
    [InlineData(int.MinValue)]
    public async Task GetLineItemById_Given_InvalidId_Returns_NotFound(int invoiceId)
    {
        // Arrange

        // Act
        var response = await _client.GetAsync($"/LineItems/{invoiceId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
