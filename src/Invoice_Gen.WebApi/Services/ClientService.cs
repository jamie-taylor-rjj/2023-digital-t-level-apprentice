using Invoice_Gen.ViewModels;

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
}
