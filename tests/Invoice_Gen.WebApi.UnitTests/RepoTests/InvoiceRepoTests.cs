using Invoice_Gen.WebApi.UnitTests.Helpers;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.RepoTests;

[ExcludeFromCodeCoverage]
public class InvoiceRepoTests
{
    [Fact]
    public void GetAll_Returns_ListOfInvoiceInstances()
    {
        // arrange
        var invoiceList = new List<Invoice>
        {
            new()
            {
                InvoiceId = 1,
                ClientId = 1,
                DueDate = new DateTime(),
                IssueDate = new DateTime(),
                VatRate = 10
            }
        };
        var invoiceListSet = DbSetHelpers.GetQueryableDbSet(invoiceList);

        var mockedRepo = new Mock<IDbContext>();
        mockedRepo.Setup(s => s.Invoices).Returns(invoiceListSet.Object);
        var mockedLogger = new Mock<ILogger<InvoiceRepository>>();

        var sut = new InvoiceRepository(mockedLogger.Object, mockedRepo.Object);

        // act
        var response = sut.GetAll();

        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<List<Invoice>>(response);
        Assert.NotEmpty(response);
    }
    
    [Fact]
    public async Task Add_AddsInstance_ToRepo()
    {
        // arrange
        var invoiceList = new List<Invoice>
        {
            new()
            {
                InvoiceId = 1,
                ClientId = 1,
            }
        };
        var invoiceListSet = DbSetHelpers.GetQueryableDbSet(invoiceList);
    
        var mockedRepo = new Mock<IDbContext>();
        mockedRepo.Setup(s => s.Invoices).Returns(invoiceListSet.Object);
        var mockedLogger = new Mock<ILogger<InvoiceRepository>>();
    
        var sut = new InvoiceRepository(mockedLogger.Object, mockedRepo.Object);
    
        var invoiceToAdd = new Invoice
        {
            ClientId = 1,
            DueDate = new DateTime(2024, 01, 01),
            IssueDate = new DateTime(2023, 06, 28),
            VatRate = 10
        };
    
        // act
        var response = await sut.Add(invoiceToAdd);
    
        // asset
        Assert.NotNull(response);
        Assert.IsAssignableFrom<Invoice>(response);
    
        Assert.Equal(invoiceToAdd.ClientId, response.ClientId);
        Assert.Equal(invoiceToAdd.DueDate, response.DueDate);
        Assert.Equal(invoiceToAdd.IssueDate, response.IssueDate);
        Assert.Equal(invoiceToAdd.VatRate, response.VatRate);
    }
}
