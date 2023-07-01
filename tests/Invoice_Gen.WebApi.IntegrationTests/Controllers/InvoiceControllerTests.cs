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
}
