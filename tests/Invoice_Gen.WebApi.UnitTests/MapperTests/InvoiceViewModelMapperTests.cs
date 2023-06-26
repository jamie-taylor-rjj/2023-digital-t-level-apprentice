namespace Invoice_Gen.WebApi.UnitTests.MapperTests;

[ExcludeFromCodeCoverage]
public class InvoiceViewModelMapperTests
{
    private readonly IMapper<InvoiceViewModel, Invoice> _mapper;
    private readonly Mock<IMapper<LineItemViewModel, LineItem>> _mockedLineItemMapper;
    private readonly Random _rng;

    public InvoiceViewModelMapperTests()
    {
        _rng = new Random();
        _mockedLineItemMapper = new Mock<IMapper<LineItemViewModel, LineItem>>();
        _mockedLineItemMapper.Setup(x =>
            x.Convert(It.IsAny<LineItem>())).Returns(new LineItemViewModel());
        _mapper = new InvoiceViewModelMapper(_mockedLineItemMapper.Object);
    }

    [Fact]
    public void Given_Invoice_Can_MapTo_InvoiceViewModel()
    {
        // Arrange
        var entity = new Invoice
        {
            ClientId = _rng.Next(0, 200),
            DueDate = new DateTime(),
            IssueDate = new DateTime(),
            VatRate = _rng.Next(10, 25),
        };

        // Act
        var vm = _mapper.Convert(entity);

        // Assert
        Assert.NotNull(vm);
        Assert.IsAssignableFrom<InvoiceViewModel>(vm);
        Assert.Equal(vm.ClientId, entity.ClientId);
        Assert.Equal(vm.DueDate, entity.DueDate);
        Assert.Equal(vm.IssueDate, entity.IssueDate);
        Assert.Equal(vm.VatRate, entity.VatRate);
    }

    [Fact]
    public void Given_InvoiceViewModel_Cannot_MapTo_InvoiceModel()
    {
        // Arrange
        var vm = new InvoiceViewModel
        {
            InvoiceId = _rng.Next(0, 200),
            ClientId = _rng.Next(0, 200),
            DueDate = new DateTime(),
            IssueDate = new DateTime(),
            VatRate = _rng.Next(10, 25),

        };

        // Act
        var exception = Record.Exception(() => _mapper.Convert(vm));

        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<NotImplementedException>(exception);
    }
}
