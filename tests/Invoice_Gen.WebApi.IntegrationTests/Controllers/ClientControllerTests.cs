namespace Invoice_Gen.WebApi.IntegrationTests.Controllers;

[ExcludeFromCodeCoverage]
public class ClientControllerTests : BaseTestClass
{
    public ClientControllerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Clients_Returns_ListOf_ClientViewModels()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync("/Clients");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var clientsList = await response.Content.ReadFromJsonAsync<List<ClientViewModel>>();
        Assert.NotNull(clientsList);
        Assert.NotEmpty(clientsList);
    }
    
    [Fact]
    public async Task GetClientById_Given_ValidId_Returns_ClientViewModel()
    {
        // Arrange
        const int clientId = 1;
        
        // Act
        var response = await _client.GetAsync($"/Clients/{clientId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var client = await response.Content.ReadFromJsonAsync<ClientViewModel>();
        Assert.NotNull(client);
        Assert.Equal("Muller Inc", client.ClientName);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-32)]
    [InlineData(int.MinValue)]
    public async Task GetClientById_Given_InvalidId_Returns_NotFound(int clientId)
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync($"/Clients/{clientId}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetPage_Given_ValidInput_Returns_PagedListOf_ClientModels()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 25;
        
        // Act
        var response = await _client.GetAsync($"Clients/Clients/page/{pageNumber}?pageSize={pageSize}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var pagedList = await response.Content.ReadFromJsonAsync<PagedResponse<ClientViewModel>>();
        Assert.NotNull(pagedList);
        Assert.NotEmpty(pagedList.Data);
        Assert.Equal(pageNumber, pagedList.PageNumber);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-32)]
    [InlineData(int.MinValue)]
    public async Task GetPage_Given_InvalidPageNumber_Returns_NotFound(int pageNumber)
    {
        // Arrange
        const int pageSize = 25;
        
        // Act
        var response = await _client.GetAsync($"Clients/Clients/page/{pageNumber}?pageSize={pageSize}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Theory]
    [InlineData(4)]
    [InlineData(84)]
    [InlineData(int.MinValue)]
    public async Task GetPage_Given_InvalidPageSize_Returns_BadRequest(int pageSize)
    {
        // Arrange
        const int pageNumber = 1;
        
        // Act
        var response = await _client.GetAsync($"Clients/Clients/page/{pageNumber}?pageSize={pageSize}");
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
