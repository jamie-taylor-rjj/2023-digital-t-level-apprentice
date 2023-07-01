using Invoice_Gen.WebApi.Services;
using Invoice_Gen.WebApi.UnitTests.Helpers;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

[ExcludeFromCodeCoverage]
public class InvoiceServiceTests
{
    private readonly Random _rng;
    private readonly int _invoiceId;
    private readonly int _clientId;
    private readonly DateTime _dueDate;
    private readonly DateTime _issueDate;
    private readonly int _vatRate;

    private readonly Mock<IMapper<InvoiceViewModel, Invoice>> _mockedInvoiceViewModelMapper;
    private readonly Mock<IMapper<InvoiceCreateModel, Invoice>> _mockedInvoiceCreateModelMapper;

    public InvoiceServiceTests()
    {
        _rng = new Random();
        _invoiceId = _rng.Next(1, 200);
        _clientId = _rng.Next(1, 200);
        _dueDate = new DateTime();
        _issueDate = new DateTime();
        _vatRate = _rng.Next(10, 25);

        _mockedInvoiceViewModelMapper = new Mock<IMapper<InvoiceViewModel, Invoice>>();
        _mockedInvoiceCreateModelMapper = new Mock<IMapper<InvoiceCreateModel, Invoice>>();
    }

    [Fact]
    public void Given_Atleast_One_Invoice_GetAll_Should_Return_At_Least_One_InvoiceViewModel()
    {
        // Arrange
        var entity = new Invoice
        {
            InvoiceId = _invoiceId,
            ClientId = _clientId,
            DueDate = _dueDate,
            IssueDate = _issueDate,
            VatRate = _vatRate
        };
        var invoicesForMock = new List<Invoice> { entity };

        var mockedRepository = new Mock<IInvoiceRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(invoicesForMock);
        var mockedLogger = new Mock<ILogger<InvoiceService>>();

        var expectedOutput = new InvoiceViewModel
        {
            InvoiceId = _invoiceId,
            ClientId = _clientId,
            DueDate = _dueDate,
            IssueDate = _issueDate,
            VatRate = _vatRate
        };

        _mockedInvoiceViewModelMapper.Setup(x => x.Convert(entity)).Returns(expectedOutput);

        var sut = new InvoiceService(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object,
            _mockedInvoiceCreateModelMapper.Object);

        // Act
        var result = sut.GetInvoices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<InvoiceViewModel>>(result);

        Assert.Equal(_invoiceId, result.FirstOrDefault()?.InvoiceId);
        Assert.Equal(_clientId, result.FirstOrDefault()?.ClientId);
        Assert.Equal(_dueDate, result.FirstOrDefault()?.DueDate);
        Assert.Equal(_issueDate, result.FirstOrDefault()?.IssueDate);
        Assert.Equal(_vatRate, result.FirstOrDefault()?.VatRate);
    }

    [Fact]
    public void Given_A_Valid_InvoiceId_GetById_Should_Return_The_Matching_InvoiceViewModel()
    {
        // Arrange
        var entity = new Invoice
        {
            InvoiceId = _invoiceId,
            ClientId = _clientId,
            DueDate = _dueDate,
            IssueDate = _issueDate,
            VatRate = _vatRate
        };
        var invoicesForMock = new List<Invoice> { entity };

        var mockedRepository = new Mock<IInvoiceRepository>();
        mockedRepository.Setup(x => x.GetAsQueryable()).Returns(invoicesForMock.AsQueryable);
        var mockedLogger = new Mock<ILogger<InvoiceService>>();

        var expectedOutput = new InvoiceViewModel
        {
            ClientId = _clientId,
            InvoiceId = _invoiceId,
            IssueDate = _issueDate,
            DueDate = _dueDate,
            VatRate = _vatRate
        };

        _mockedInvoiceViewModelMapper.Setup(x => x.Convert(entity)).Returns(expectedOutput);

        var sut = new InvoiceService(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object,
            _mockedInvoiceCreateModelMapper.Object);

        // Act
        var result = sut.GetById(_invoiceId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<InvoiceViewModel>(result);

        Assert.Equal(_invoiceId, result.InvoiceId);
        Assert.Equal(_clientId, result.ClientId);
        Assert.Equal(_dueDate, result.DueDate);
        Assert.Equal(_issueDate, result.IssueDate);
        Assert.Equal(_vatRate, result.VatRate);
        Assert.Empty(result.LineItems);
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
        var mockedLogger = new Mock<ILogger<InvoiceService>>();

        _mockedInvoiceViewModelMapper.Setup(x =>
            x.Convert(It.IsAny<Invoice>())).Returns(It.IsAny<InvoiceViewModel>());

        var sut = new InvoiceService(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object,
            _mockedInvoiceCreateModelMapper.Object);

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
        var mockedLogger = new Mock<ILogger<InvoiceService>>();

        _mockedInvoiceViewModelMapper.Setup(x =>
            x.Convert(It.IsAny<Invoice>())).Returns(It.IsAny<InvoiceViewModel>());

        var sut = new InvoiceService(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object,
            _mockedInvoiceCreateModelMapper.Object);

        // Act
        var result = sut.GetPage(pageNumber, pageSize);
        
        // Assert
        Assert.IsAssignableFrom<PagedResponse<InvoiceViewModel>>(result);
        Assert.Equal(pageSize, result.Data.Count);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(numberOfClients / pageSize, result.TotalPages);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Fact]
    public async Task Given_Valid_InvoiceCreationModel_CreateInvoice_Returns_IdOfNewInvoice()
    {
        // Arrange
        var entity = new Invoice
        {
            InvoiceId = _invoiceId,
            ClientId = _clientId,
            DueDate = _dueDate,
            IssueDate = _issueDate,
            VatRate = _vatRate
        };

        var invoiceToAdd = new Invoice
        {
            InvoiceId = 7,
            ClientId = _clientId,
            DueDate = _dueDate,
            IssueDate = _issueDate,
            VatRate = _vatRate,
            LineItems = new List<LineItem>
            {
                new()
                {
                    Cost = 10,
                    Description = Guid.NewGuid().ToString(),
                    Quantity = 1
                }
            }
        };
        var invoicesForMock = new List<Invoice> { entity };
        var mockedRepository = new Mock<IInvoiceRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(invoicesForMock);
        mockedRepository.Setup(x => x.Add(It.IsAny<Invoice>())).Returns(Task.FromResult(invoiceToAdd));

        _mockedInvoiceCreateModelMapper.Setup(x => x.Convert(It.IsAny<InvoiceCreateModel>())).Returns(invoiceToAdd);
        var mockedLogger = new Mock<ILogger<InvoiceService>>();

        var sut = new InvoiceService(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object,
            _mockedInvoiceCreateModelMapper.Object);

        // act
        var response = await sut.CreateNewInvoice(new InvoiceCreateModel
        {
            ClientId = _rng.Next(1, 200),
            IssueDate = new DateTime(),
            DueDate = new DateTime(),
            VatRate = _rng.Next(10, 25),
            LineItems = new List<LineItemViewModel>
            {
                new()
                {
                    Cost = 10,
                    Description = Guid.NewGuid().ToString(),
                    Quantity = 1
                }
            }
        });

        // Assert
        Assert.NotEqual(0, response);
    }

    [Fact]
    public async Task Given_Valid_InvoiceId_DeleteClient_DoesntRaiseException()
    {
        // Arrange
        var invoice = new Invoice
        {
            InvoiceId = 1,
            ClientId = 1,
            IssueDate = default,
            DueDate = default,
            VatRate = 10,
            LineItems = new List<LineItem>(),

        };
        var invoicesForMock = new List<Invoice> { invoice };
        var mockedRepository = new Mock<IInvoiceRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(invoicesForMock);
        mockedRepository.Setup(x => x.Delete(It.IsAny<int>()));
        var mockedLogger = new Mock<ILogger<InvoiceService>>();

        var sut = new InvoiceService(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object,
            _mockedInvoiceCreateModelMapper.Object);

        // act
        var exception = await Record.ExceptionAsync(() => sut.DeleteInvoice(1));

        // Assert
        Assert.Null(exception);
    }
}
