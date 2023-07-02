namespace InvoiceGen.Services.ClientServices;

public class ClientGetter : IGetClients
{
    private readonly ILogger<ClientGetter> _logger;
    private readonly IClientRepository _clientRepository;
    private readonly IMapper<ClientViewModel, Client> _clientViewModelMapper;

    public ClientGetter(ILogger<ClientGetter> logger, IClientRepository clientRepository,
        IMapper<ClientViewModel, Client> clientViewModelMapper)
    {
        _logger = logger;
        _clientRepository = clientRepository;
        _clientViewModelMapper = clientViewModelMapper;
    }

    public List<ClientViewModel> GetClients()
    {
        using (_logger.BeginScope("{ClientService} getting all clients", nameof(ClientPager)))
        {
            var all = _clientRepository.GetAll();

            _logger.LogInformation("Retrieved {Count} {ClientModel}", all.Count, nameof(Client));

            _logger.LogInformation("Converting to List of {ClientNameViewModel} using {Mapper}",
                nameof(ClientViewModel), typeof(ClientNameViewModelMapper));
            var returnData = all.Select(c => _clientViewModelMapper.Convert(c)).ToList();

            _logger.LogInformation("Returning {count} of {ClientNameViewModel} instances", returnData.Count,
                nameof(ClientViewModel));
            return returnData;
        }
    }

    public ClientViewModel? GetById(int id)
    {
        using (_logger.BeginScope("{ClientService} getting client record for {ID}", nameof(ClientPager), id))
        {
            var client = _clientRepository.GetAsQueryable().FirstOrDefault(f => f.ClientId == id);

            _logger.LogInformation("Returning {ClientNameViewModel} for {ID}", nameof(ClientViewModel), id);
            return client == null ? null : _clientViewModelMapper.Convert(client);
        }
    }
}
