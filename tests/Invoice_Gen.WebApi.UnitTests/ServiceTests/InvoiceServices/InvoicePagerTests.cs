using Invoice_Gen.WebApi.UnitTests.Helpers;
using InvoiceGen.Services.InvoiceServices;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests.InvoiceServices;

public class InvoicePagerTests
{
    private readonly Mock<IMapper<InvoiceViewModel, Invoice>> _mockedInvoiceViewModelMapper;

    public InvoicePagerTests()
    {
        _mockedInvoiceViewModelMapper = new Mock<IMapper<InvoiceViewModel, Invoice>>();
    }

    [Fact]
    public void Given_ValidInput_GetPage_Returns_Valid_PagedResponseOfClientViewModel()
    {
        // Arrange
        const int numberOfClients = 200;
        const int pageNumber = 1;
        const int pageSize = 10;
        var invoicesForMock = InvoiceHelpers.GenerateRandomListOfInvoices(200);
        var mockedRepository = new Mock<IInvoiceRepository>();
        mockedRepository.Setup(x => x.GetAsQueryable()).Returns(invoicesForMock.AsQueryable());
        var mockedLogger = new Mock<ILogger<InvoicePager>>();

        _mockedInvoiceViewModelMapper.Setup(x =>
            x.Convert(It.IsAny<Invoice>())).Returns(It.IsAny<InvoiceViewModel>());

        var sut = new InvoicePager(mockedLogger.Object, mockedRepository.Object,
            _mockedInvoiceViewModelMapper.Object);

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
        var mockedRepository = new Mock<IInvoiceRepository>();
        mockedRepository.Setup(x => x.GetAsQueryable()).Returns(invoicesForMock.AsQueryable());
        var mockedLogger = new Mock<ILogger<InvoicePager>>();

        _mockedInvoiceViewModelMapper.Setup(x =>
            x.Convert(It.IsAny<Invoice>())).Returns(It.IsAny<InvoiceViewModel>());

        var sut = new InvoicePager(mockedLogger.Object, mockedRepository.Object,
            _mockedInvoiceViewModelMapper.Object);

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
