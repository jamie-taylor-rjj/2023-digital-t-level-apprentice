﻿using InvoiceGen.Services.InvoiceServices;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests.InvoiceServices;

public class InvoiceGetterTests
{
    private readonly int _invoiceId;
    private readonly int _clientId;
    private readonly DateTime _dueDate;
    private readonly DateTime _issueDate;
    private readonly int _vatRate;
    private readonly Mock<IMapper<InvoiceViewModel, Invoice>> _mockedInvoiceViewModelMapper;

    public InvoiceGetterTests()
    {
        var rng = new Random();
        _invoiceId = rng.Next(1, 200);
        _clientId = rng.Next(1, 200);
        _dueDate = new DateTime();
        _issueDate = new DateTime();
        _vatRate = rng.Next(10, 25);

        _mockedInvoiceViewModelMapper = new Mock<IMapper<InvoiceViewModel, Invoice>>();
    }

    [Fact]
    public void Given_AtLeast_One_Invoice_GetAll_Should_Return_At_Least_One_InvoiceViewModel()
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
        var mockedLogger = new Mock<ILogger<InvoiceGetter>>();

        var expectedOutput = new InvoiceViewModel
        {
            InvoiceId = _invoiceId,
            ClientId = _clientId,
            DueDate = _dueDate,
            IssueDate = _issueDate,
            VatRate = _vatRate
        };

        _mockedInvoiceViewModelMapper.Setup(x => x.Convert(entity)).Returns(expectedOutput);

        var sut = new InvoiceGetter(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object);

        // Act
        var result = sut.GetInvoices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<InvoiceViewModel>>(result);

        Assert.Equal(_invoiceId, result.FirstOrDefault()?.InvoiceId);
        Assert.Equal(_clientId, result.FirstOrDefault()?.ClientId);
        Assert.Equal(_dueDate, result.FirstOrDefault()?.DueDate);
        Assert.Equal(_issueDate, result.FirstOrDefault()?.IssueDate);
        Assert.Equal(_vatRate, result.FirstOrDefault()?.VatRate);
    }

    [Fact]
    public void Given_A_Valid_InvoiceId_GetById_Should_Return_The_Matching_InvoiceViewModel()
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
        mockedRepository.Setup(x => x.GetAsQueryable()).Returns(invoicesForMock.AsQueryable);
        var mockedLogger = new Mock<ILogger<InvoiceGetter>>();

        var expectedOutput = new InvoiceViewModel
        {
            ClientId = _clientId,
            InvoiceId = _invoiceId,
            IssueDate = _issueDate,
            DueDate = _dueDate,
            VatRate = _vatRate
        };

        _mockedInvoiceViewModelMapper.Setup(x => x.Convert(entity)).Returns(expectedOutput);

        var sut = new InvoiceGetter(mockedLogger.Object, mockedRepository.Object, _mockedInvoiceViewModelMapper.Object);

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
        Assert.Empty(result.LineItems);
    }
}
