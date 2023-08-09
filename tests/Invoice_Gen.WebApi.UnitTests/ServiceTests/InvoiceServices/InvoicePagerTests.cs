namespace Invoice_Gen.WebApi.UnitTests.ServiceTests.InvoiceServices;

public class InvoicePagerTests
{
    private readonly IMapper<InvoiceViewModel, Invoice> _mockedInvoiceViewModelMapper =
        Substitute.For<IMapper<InvoiceViewModel, Invoice>>();

    [Fact]
    public void Given_ValidInput_GetPage_Returns_Valid_PagedResponseOfClientViewModel()
    {
        // Arrange
        const int numberOfClients = 200;
        const int pageNumber = 1;
        const int pageSize = 10;
        var invoicesForMock = InvoiceHelpers.GenerateRandomListOfInvoices(200);
        var mockedRepository = Substitute.For<IInvoiceRepository>();
        mockedRepository.GetAsQueryable().ReturnsForAnyArgs(invoicesForMock.AsQueryable());
        var mockedLogger = Substitute.For<ILogger<InvoicePager>>();

        _mockedInvoiceViewModelMapper.Convert(new Invoice()).ReturnsForAnyArgs(new InvoiceViewModel());

        var sut = new InvoicePager(mockedLogger, mockedRepository,
            _mockedInvoiceViewModelMapper);

        // Act
        var result = sut.GetPage(pageNumber, pageSize);

        // Assert
        Assert.IsAssignableFrom<PagedResponse<InvoiceViewModel>>(result);
        Assert.Equal(pageSize, result.Data.Count);
        Assert.Equal(pageNumber, result.PageNumber);
        Assert.Equal(numberOfClients / pageSize, result.TotalPages);
        Assert.Equal(numberOfClients, result.TotalRecords);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public void Given_PageNumberLessThanOne_GetPage_Returns_FirstPageOf_PagedResponseOfClientViewModel(int pageNumber)
    {
        // Arrange
        const int numberOfClients = 200;
        const int pageSize = 10;
        var invoicesForMock = InvoiceHelpers.GenerateRandomListOfInvoices(200);
        var mockedRepository = Substitute.For<IInvoiceRepository>();
        mockedRepository.GetAsQueryable().ReturnsForAnyArgs(invoicesForMock.AsQueryable());
        var mockedLogger = Substitute.For<ILogger<InvoicePager>>();

        _mockedInvoiceViewModelMapper.Convert(new Invoice()).ReturnsForAnyArgs(new InvoiceViewModel());

        var sut = new InvoicePager(mockedLogger, mockedRepository,
            _mockedInvoiceViewModelMapper);

        // Act
        var result = sut.GetPage(pageNumber, pageSize);

        // Assert
        Assert.IsAssignableFrom<PagedResponse<InvoiceViewModel>>(result);
        Assert.Equal(pageSize, result.Data.Count);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(numberOfClients / pageSize, result.TotalPages);
        Assert.Equal(pageSize, result.PageSize);
    }
}
