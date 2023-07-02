using InvoiceGen.Services.InvoiceServices;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests.InvoiceServices;

public class InvoiceDeleterTests
{
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
        var mockedLogger = new Mock<ILogger<InvoiceDeleter>>();

        var sut = new InvoiceDeleter(mockedLogger.Object, mockedRepository.Object);

        // act
        var exception = await Record.ExceptionAsync(() => sut.DeleteInvoice(1));

        // Assert
        Assert.Null(exception);
    }
}
