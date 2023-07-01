using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.WebApi.Services;

public class ClientService : IClientService
{
    private readonly IMapper<ClientViewModel, Client> _clientViewModelMapper;
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientService> _logger;
    public ClientService(IMapper<ClientViewModel, Client> clientViewModelMapper,
        IClientRepository clientRepository, ILogger<ClientService> logger)
    {
        _clientViewModelMapper = clientViewModelMapper;
        _clientRepository = clientRepository;
        _logger = logger;
    }
    public List<ClientViewModel> GetClients()
    {
        using (_logger.BeginScope("{ClientService} getting all clients", nameof(ClientService)))
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
        using (_logger.BeginScope("{ClientService} getting client record for {ID}", nameof(ClientService), id))
        {
            var client = _clientRepository.GetAsQueryable().FirstOrDefault(f => f.ClientId == id);

            _logger.LogInformation("Returning {ClientNameViewModel} for {ID}", nameof(ClientViewModel), id);
            return client == null ? null : _clientViewModelMapper.Convert(client);
        }
    }

    public PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10)
    {
        using (_logger.BeginScope(
                   "{NameOfService} creating paged response of {ViewModelName} with page number of {PageNumber} and page size of {PageSize}",
                   nameof(ClientService), nameof(ClientViewModel), pageNumber, pageSize))
        {
            var pageNumberToUse = pageNumber < 1
                ? 1
                : pageNumber;

            var records = _clientRepository.GetAsQueryable();

            var totalCount = records.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var page = records
                .OrderBy(c => c.ClientId)
                .Skip((pageNumberToUse - 1) * pageSize)
                .Take(pageSize);

            return new PagedResponse<ClientViewModel>
            {
                Data = page.AsEnumerable().Select(_clientViewModelMapper.Convert).ToList(),
                PageNumber = pageNumberToUse,
                PageSize = page.Count(),
                TotalPages = totalPages,
                TotalRecords = totalCount
            };
        }
    }

    public async Task<int> CreateNewClient(ClientCreationModel inputClient)
    {
        using (_logger.BeginScope("{ClientService} creating new client record for {ClientName}", nameof(ClientService),
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

    public async Task DeleteClient(int clientId)
    {
        _logger.LogInformation("Deleting client with ID of {ClientId}", clientId);
        await _clientRepository.Delete(clientId);
    }
}
