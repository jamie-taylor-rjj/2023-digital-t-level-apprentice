using Invoice_Gen.WebApi.UnitTests.Helpers;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.RepoTests;

public class LineItemRepoTests
{
    [Fact]
    public void GetAll_Returns_ListOfLineItemInstances()
    {
        // arrange
        var lineItemList = new List<LineItem>
        {
            new()
            {
                InvoiceId = 1,
                Cost = 100,
                Description = Guid.NewGuid().ToString(),
                Quantity = 1
            }
        };
        var lineItemListSet = DbSetHelpers.GetQueryableDbSet(lineItemList);

        var mockedRepo = new Mock<IDbContext>();
        mockedRepo.Setup(s => s.LineItems).Returns(lineItemListSet.Object);
        var mockedLogger = new Mock<ILogger<LineItemRepository>>();

        var sut = new LineItemRepository(mockedLogger.Object, mockedRepo.Object);

        // act
        var response = sut.GetAll();

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<List<LineItem>>(response);
        Assert.NotEmpty(response);
    }
}
