namespace InvoiceGen.Services.ClientServices;

public class ClientDeleter : IDeleteClients
{
    private readonly ILogger<ClientDeleter> _logger;
    private readonly IClientRepository _clientRepository;

    public ClientDeleter(ILogger<ClientDeleter> logger, IClientRepository clientRepository)
    {
        _logger = logger;
        _clientRepository = clientRepository;
    }
    
    public async Task DeleteClient(int clientId)
    {
        _logger.LogInformation("Deleting client with ID of {ClientId}", clientId);
        await _clientRepository.Delete(clientId);
    }
}
