namespace Invoice_Gen.WebApi.UnitTests.MapperTests;

public class InvoiceCreateViewModelMapperTests
{
    private readonly IMapper<InvoiceCreateModel, Invoice> _mapper = new InvoiceCreateModelMapper();
    private readonly Random _rng = new();

    [Fact]
    public void Given_InvoiceCreateViewModel_Can_MapTo_InvoiceModel()
    {
        // Arrange
        var vm = new InvoiceCreateModel
        {
            ClientId = _rng.Next(0, 200),
            DueDate = new DateTime(),
            IssueDate = new DateTime(),
            VatRate = _rng.Next(10, 25),
            LineItems = new List<LineItemViewModel>
            {
                new()
                {
                    Cost = _rng.Next(0, 200),
                    Description = Guid.NewGuid().ToString(),
                    Quantity = _rng.Next(0, 200)
                }
            }
        };

        // Act
        var entity = _mapper.Convert(vm);

        // Assert
        Assert.NotNull(entity);
        Assert.IsAssignableFrom<Invoice>(entity);
        Assert.Equal(vm.ClientId, entity.ClientId);
        Assert.Equal(vm.DueDate, entity.DueDate);
        Assert.Equal(vm.IssueDate, entity.IssueDate);
        Assert.Equal(vm.VatRate, entity.VatRate);
        Assert.Single(entity.LineItems);
    }

    [Fact]
    public void Given_InvoiceModel_Cannot_MapTo_InvoiceViewModel()
    {
        // Arrange
        var vm = new Invoice
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
