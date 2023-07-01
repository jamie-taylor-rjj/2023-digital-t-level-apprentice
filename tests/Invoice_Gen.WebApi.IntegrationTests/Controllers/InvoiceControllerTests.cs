using System.Text;
using System.Text.Json;

namespace Invoice_Gen.WebApi.IntegrationTests.Controllers;

[ExcludeFromCodeCoverage]
public class InvoiceControllerTests : BaseTestClass
{
    public InvoiceControllerTests(CustomWebApplicationFactory factory) : base(factory) { }
    
    [Fact]
    public async Task Invoices_Returns_ListOf_InvoiceViewModels()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync("/Invoice");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var invoiceData = await response.Content.ReadFromJsonAsync<List<InvoiceViewModel>>();
        Assert.NotNull(invoiceData);
        Assert.NotEmpty(invoiceData);
    }
    
    [Fact]
    public async Task GetInvoiceById_Given_ValidId_Returns_InvoiceViewModel()
    {
        // Arrange
        const int invoiceId = 1;
        
        // Act
        var response = await _client.GetAsync($"/Invoice/{invoiceId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var invoice = await response.Content.ReadFromJsonAsync<InvoiceViewModel>();
        Assert.NotNull(invoice);
        Assert.Equal(25, invoice.VatRate);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-32)]
    [InlineData(int.MinValue)]
    public async Task GetInvoiceById_Given_InvalidId_Returns_NotFound(int invoiceId)
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync($"/Invoice/{invoiceId}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task GetPage_Given_ValidInput_Returns_PagedListOf_InvoiceViewModels()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 25;
        
        // Act
        var response = await _client.GetAsync($"Invoice/page/{pageNumber}?pageSize={pageSize}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var pagedList = await response.Content.ReadFromJsonAsync<PagedResponse<InvoiceViewModel>>();
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
        var response = await _client.GetAsync($"Invoice/page/{pageNumber}?pageSize={pageSize}");
        
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
        var response = await _client.GetAsync($"Invoice/page/{pageNumber}?pageSize={pageSize}");
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task CreateInvoice_Given_ValidInput_Returns_OK()
    {
        // Arrange
        var newInvoice = new InvoiceCreateModel
        {
            ClientId = 1,
            IssueDate = default,
            DueDate = default,
            VatRate = 10,
            LineItems = new List<LineItemViewModel>()
        };
        
        var json = JsonSerializer.Serialize(newInvoice);
        var content = new StringContent(json, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        
        // Act
        var response = await _client.PutAsync("/Invoice", content);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(data);
    }

    [Fact]
    public async Task Delete_Given_ValidInput_Returns_OK()
    {
        // Arrange
        const int invoiceId = 5; // because other tests in this Fixture use InvoiceId 1;
        // A new in memory database is spun up per text fixture, so this isn't a huge
        // issue for this app

        // Act
        var response = await _client.DeleteAsync($"/Invoice/{invoiceId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-32)]
    [InlineData(int.MinValue)]
    public async Task Delete_Given_InvalidInput_Returns_BadRequest(int invoiceId)
    {
        // Arrange

        // Act
        var response = await _client.DeleteAsync($"/Invoice/{invoiceId}");
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
