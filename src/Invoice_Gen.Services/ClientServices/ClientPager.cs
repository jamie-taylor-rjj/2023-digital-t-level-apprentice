namespace InvoiceGen.Services.ClientServices;

public class ClientPager : IPageClients
{
    private readonly IMapper<ClientViewModel, Client> _clientViewModelMapper;
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientPager> _logger;
    public ClientPager(IMapper<ClientViewModel, Client> clientViewModelMapper,
        IClientRepository clientRepository, ILogger<ClientPager> logger)
    {
        _clientViewModelMapper = clientViewModelMapper;
        _clientRepository = clientRepository;
        _logger = logger;
    }

    public PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10)
    {
        using (_logger.BeginScope(
                   "{NameOfService} creating paged response of {ViewModelName} with page number of {PageNumber} and page size of {PageSize}",
                   nameof(ClientPager), nameof(ClientViewModel), pageNumber, pageSize))
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

    public async Task DeleteClient(int clientId)
    {
        _logger.LogInformation("Deleting client with ID of {ClientId}", clientId);
        await _clientRepository.Delete(clientId);
    }
}
