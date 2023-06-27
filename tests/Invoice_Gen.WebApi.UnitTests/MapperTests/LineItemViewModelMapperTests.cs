namespace Invoice_Gen.WebApi.UnitTests.MapperTests;

[ExcludeFromCodeCoverage]
public class LineItemViewModelMapperTests
{
    private readonly IMapper<LineItemViewModel, LineItem> _mapper;
    private readonly Random _rng;

    public LineItemViewModelMapperTests()
    {
        _rng = new Random();
        _mapper = new LineItemViewModelMapper();
    }

    [Fact]
    public void Given_LineItem_Can_MapTo_LineItemViewModel()
    {
        // Arrange
        var entity = new LineItem
        {
            LineItemId = _rng.Next(0, 200),
            InvoiceId = _rng.Next(0, 200),
            Cost = _rng.Next(0, 200),
            Description = Guid.NewGuid().ToString(),
            Quantity = _rng.Next(0, 200),
        };

        // Act
        var vm = _mapper.Convert(entity);

        // Assert
        Assert.NotNull(vm);
        Assert.IsAssignableFrom<LineItemViewModel>(vm);
        Assert.Equal(vm.LineItemId, entity.LineItemId);
        Assert.Equal(vm.InvoiceId, entity.InvoiceId);
        Assert.Equal(vm.Cost, entity.Cost);
        Assert.Equal(vm.Description, entity.Description);
        Assert.Equal(vm.Quantity, entity.Quantity);
    }

    [Fact]
    public void Given_LineItemViewModel_Cannot_MapTo_LineItemModel()
    {
        // Arrange
        var vm = new LineItemViewModel
        {
            LineItemId = _rng.Next(0, 200),
            InvoiceId = _rng.Next(0, 200),
            Cost = _rng.Next(0, 200),
            Description = Guid.NewGuid().ToString(),
            Quantity = _rng.Next(0, 200)
        };

        // Act
        var exception = Record.Exception(() => _mapper.Convert(vm));

        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<NotImplementedException>(exception);
    }
}
