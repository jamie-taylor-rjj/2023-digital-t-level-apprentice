using Invoice_Gen.WebApi.Services;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

public class LineItemServiceTests
{
    private readonly Random _rng;
    private readonly int _lineItemId;
    private readonly int _invoiceId;
    private readonly int _cost;
    private readonly string _description;
    private readonly int _quantity;

    private readonly Mock<IMapper<LineItemViewModel, LineItem>> _mockedLineItemViewModelMapper;

    public LineItemServiceTests()
    {
        _rng = new Random();
        _lineItemId = _rng.Next(1, 200);
        _invoiceId = _rng.Next(1, 200);
        _cost = _rng.Next(1, 200);
        _description = Guid.NewGuid().ToString();
        _quantity = _rng.Next(1, 25);

        _mockedLineItemViewModelMapper = new Mock<IMapper<LineItemViewModel, LineItem>>();
    }

    [Fact]
    public void Given_Atleast_One_LineItem_GetAll_Should_Return_At_Least_One_LineItemViewModel()
    {
        // Arrange
        var entity = new LineItem
        {
            LineItemId = _lineItemId,
            InvoiceId = _invoiceId,
            Cost = _cost,
            Description = _description,
            Quantity = _quantity
        };
        var lineItemsForMock = new List<LineItem> { entity };

        var mockedRepository = new Mock<ILineItemRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(lineItemsForMock);
        var mockedLogger = new Mock<ILogger<LineItemService>>();

        var expectedOutput = new LineItemViewModel
        {
            LineItemId = _lineItemId,
            InvoiceId = _invoiceId,
            Cost = _cost,
            Description = _description,
            Quantity = _quantity
        };

        _mockedLineItemViewModelMapper.Setup(x => x.Convert(entity)).Returns(expectedOutput);

        var sut = new LineItemService(mockedLogger.Object, mockedRepository.Object, _mockedLineItemViewModelMapper.Object);

        // Act
        var result = sut.GetById(_lineItemId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<LineItemViewModel>(result);

        Assert.Equal(_lineItemId, result.LineItemId);
        Assert.Equal(_invoiceId, result.InvoiceId);
        Assert.Equal(_cost, result.Cost);
        Assert.Equal(_description, result.Description);
        Assert.Equal(_quantity, result.Quantity);
    }
}

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
        var result = sut.GetById(_invoiceId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<InvoiceViewModel>(result);

        Assert.Equal(_invoiceId, result.InvoiceId);
        Assert.Equal(_clientId, result.ClientId);
        Assert.Equal(_dueDate, result.DueDate);
        Assert.Equal(_issueDate, result.IssueDate);
        Assert.Equal(_vatRate, result.VatRate);
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
            VatRate = _vatRate
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
            LineItems = new List<LineItemViewModel>()
        });

        // Assert
        Assert.NotEqual(0, response);
    }
}
