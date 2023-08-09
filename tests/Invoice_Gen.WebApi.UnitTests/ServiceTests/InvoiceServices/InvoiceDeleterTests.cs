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
        var mockedRepository = Substitute.For<IInvoiceRepository>();
        mockedRepository.GetAll().ReturnsForAnyArgs(invoicesForMock);
        mockedRepository.Delete(0).ReturnsForAnyArgs(Task.FromResult(0));
        var mockedLogger = Substitute.For<ILogger<InvoiceDeleter>>();

        var sut = new InvoiceDeleter(mockedLogger, mockedRepository);

        // act
        var exception = await Record.ExceptionAsync(() => sut.DeleteInvoice(1));

        // Assert
        Assert.Null(exception);
    }
}
