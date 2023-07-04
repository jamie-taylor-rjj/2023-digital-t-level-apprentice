﻿using Invoice_Gen.WebApi.UnitTests.Helpers;
using InvoiceGen.Services.ClientServices;
using Microsoft.Extensions.Logging;

namespace Invoice_Gen.WebApi.UnitTests.ServiceTests;

public class ClientServiceTests
{
    private readonly Random _rng;
    private readonly int _clientId;
    private readonly string _clientName;
    private readonly string _clientAddress;
    private readonly string _contactName;
    private readonly string _contactEmail;

    private readonly Mock<IMapper<ClientViewModel, Client>> _mockedClientNameViewModelMapper;

    public ClientServiceTests()
    {
        _rng = new Random();
        _clientId = _rng.Next(1, 200);
        _clientName = Guid.NewGuid().ToString();
        _clientAddress = Guid.NewGuid().ToString();
        _contactName = Guid.NewGuid().ToString();
        _contactEmail = Guid.NewGuid().ToString();

        _mockedClientNameViewModelMapper = new Mock<IMapper<ClientViewModel, Client>>();
    }

    [Fact]
    public void Given_ValidInput_GetPage_Returns_Valid_PagedResponseOfClientViewModel()
    {
        // Arrange
        const int numberOfClients = 200;
        const int pageNumber = 1;
        const int pageSize = 10;
        var clientsForMock = ClientHelpers.GenerateRandomListOfClients(200);
        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAsQueryable()).Returns(clientsForMock.AsQueryable());
        var mockedLogger = new Mock<ILogger<ClientPager>>();

        _mockedClientNameViewModelMapper.Setup(x =>
            x.Convert(It.IsAny<Client>())).Returns(It.IsAny<ClientViewModel>());

        var sut = new ClientPager(_mockedClientNameViewModelMapper.Object,
            mockedRepository.Object, mockedLogger.Object);

        // Act
        var result = sut.GetPage(pageNumber, pageSize);

        // Assert
        Assert.IsAssignableFrom<PagedResponse<ClientViewModel>>(result);
        Assert.Equal(pageSize, result.Data.Count);
        Assert.Equal(pageNumber, result.PageNumber);
        Assert.Equal(numberOfClients / pageSize, result.TotalPages);
        Assert.Equal(numberOfClients, result.TotalRecords);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public void Given_PageNumberLessThanOne_GetPage_Returns_FirstPageOf_PagedResponseOfClientViewModel(int pageNumber)
    {
        // Arrange
        const int numberOfClients = 200;
        const int pageSize = 10;
        var clientsForMock = ClientHelpers.GenerateRandomListOfClients(200);
        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAsQueryable()).Returns(clientsForMock.AsQueryable());
        var mockedLogger = new Mock<ILogger<ClientPager>>();

        _mockedClientNameViewModelMapper.Setup(x =>
            x.Convert(It.IsAny<Client>())).Returns(It.IsAny<ClientViewModel>());

        var sut = new ClientPager(_mockedClientNameViewModelMapper.Object,
            mockedRepository.Object, mockedLogger.Object);

        // Act
        var result = sut.GetPage(pageNumber, pageSize);

        // Assert
        Assert.IsAssignableFrom<PagedResponse<ClientViewModel>>(result);
        Assert.Equal(pageSize, result.Data.Count);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(numberOfClients / pageSize, result.TotalPages);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Fact]
    public async Task Given_Valid_ClientId_DeleteClient_DoesntRaiseException()
    {
        // Arrange
        var client = new Client
        {
            ClientId = 1,
            ClientName = _clientName,
            ClientAddress = _clientAddress,
            ContactName = _contactName,
            ContactEmail = _contactEmail
        };
        var clientsForMock = new List<Client> { client };
        var mockedRepository = new Mock<IClientRepository>();
        mockedRepository.Setup(x => x.GetAll()).Returns(clientsForMock);
        mockedRepository.Setup(x => x.Delete(It.IsAny<int>()));
        var mockedLogger = new Mock<ILogger<ClientPager>>();

        var sut = new ClientPager(_mockedClientNameViewModelMapper.Object, mockedRepository.Object, mockedLogger.Object);

        // act
        var exception = await Record.ExceptionAsync(() => sut.DeleteClient(1));

        // Assert
        Assert.Null(exception);
    }
}
