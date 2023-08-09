namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

public class LineItemServiceTests
{
    private readonly Random _rng;
    private readonly int _lineItemId;
    private readonly int _invoiceId;
    private readonly int _cost;
    private readonly string _description;
    private readonly int _quantity;

    private readonly IMapper<LineItemViewModel, LineItem> _mockedLineItemViewModelMapper;

    public LineItemServiceTests()
    {
        _rng = new Random();
        _lineItemId = _rng.Next(1, 200);
        _invoiceId = _rng.Next(1, 200);
        _cost = _rng.Next(1, 200);
        _description = Guid.NewGuid().ToString();
        _quantity = _rng.Next(1, 25);

        _mockedLineItemViewModelMapper = Substitute.For<IMapper<LineItemViewModel, LineItem>>();
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

        var mockedRepository = Substitute.For<ILineItemRepository>();
        mockedRepository.GetAll().ReturnsForAnyArgs(lineItemsForMock);
        var mockedLogger = Substitute.For<ILogger<LineItemGetter>>();

        var expectedOutput = new LineItemViewModel
        {
            LineItemId = _lineItemId,
            InvoiceId = _invoiceId,
            Cost = _cost,
            Description = _description,
            Quantity = _quantity
        };

        _mockedLineItemViewModelMapper.Convert(entity).ReturnsForAnyArgs(expectedOutput);

        var sut = new LineItemGetter(mockedLogger, mockedRepository, _mockedLineItemViewModelMapper);

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
