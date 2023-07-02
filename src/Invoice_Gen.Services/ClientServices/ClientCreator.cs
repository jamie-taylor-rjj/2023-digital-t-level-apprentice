namespace InvoiceGen.Services.ClientServices;

public class ClientCreator : ICreateClients
{
    private readonly ILogger<ClientCreator> _logger;
    private readonly IClientRepository _clientRepository;

    public ClientCreator(ILogger<ClientCreator> logger, IClientRepository clientRepository)
    {
        _logger = logger;
        _clientRepository = clientRepository;
    }

    public async Task<int> CreateNewClient(ClientCreationModel inputClient)
    {
        using (_logger.BeginScope("{ClientService} creating new client record for {ClientName}", nameof(ClientPager),
                   inputClient.ClientName))
        {
            var response = await _clientRepository.Add(new Client
            {
                ClientAddress = inputClient.ClientAddress,
                ClientName = inputClient.ClientName,
                ContactEmail = inputClient.ContactEmail,
                ContactName = inputClient.ContactName
            });
            _logger.LogInformation("Generated ID of new client is {ClientId}", response.ClientId);
            return response.ClientId;
        }
    }
}
