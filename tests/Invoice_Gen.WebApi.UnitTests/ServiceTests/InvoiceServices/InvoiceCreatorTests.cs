using InvoiceGen.Services.InvoiceServices;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests.InvoiceServices;

public class InvoiceCreatorTests
{
    private readonly Random _rng;
    private readonly int _invoiceId;
    private readonly int _clientId;
    private readonly DateTime _dueDate;
    private readonly DateTime _issueDate;
    private readonly int _vatRate;

    private readonly Mock<IMapper<InvoiceCreateModel, Invoice>> _mockedInvoiceCreateModelMapper;

    public InvoiceCreatorTests()
    {
        _rng = new Random();
        _invoiceId = _rng.Next(1, 200);
        _clientId = _rng.Next(1, 200);
        _dueDate = new DateTime();
        _issueDate = new DateTime();
        _vatRate = _rng.Next(10, 25);

        _mockedInvoiceCreateModelMapper = new Mock<IMapper<InvoiceCreateModel, Invoice>>();
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
        var mockedLogger = new Mock<ILogger<InvoiceCreator>>();

        var sut = new InvoiceCreator(mockedLogger.Object, mockedRepository.Object,
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
}
