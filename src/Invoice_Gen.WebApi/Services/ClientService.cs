using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.WebApi.Services;

public class ClientService : IClientService
{
    private readonly IMapper<ClientNameViewModel, Client> _clientViewModelMapper;
    private readonly IClientRepository _clientRepository;
    public ClientService(IMapper<ClientNameViewModel, Client> clientViewModelMapper,
        IClientRepository clientRepository)
    {
        _clientViewModelMapper = clientViewModelMapper;
        _clientRepository = clientRepository;
    }
    public List<ClientNameViewModel> GetClients()
    {
        return _clientRepository.GetAll().Select(c => _clientViewModelMapper.Convert(c)).ToList();
    }

    public ClientNameViewModel? GetById(int id)
    {
        var client = _clientRepository.GetAll().FirstOrDefault(f => f.ClientId == id);
        return client == null ? null : _clientViewModelMapper.Convert(client);
    }

    public async Task<int> CreateNewClient(ClientCreationModel inputClient)
    {
        var response = await _clientRepository.Add(new()
        {
            ClientAddress = inputClient.ClientAddress,
            ClientName = inputClient.ClientName,
            ContactEmail = inputClient.ContactEmail,
            ContactName = inputClient.ContactEmail
        });
        return response.ClientId;
    }

    public async Task DeleteClient(int clientId)
    {
        await _clientRepository.Delete(clientId);
    }
}
