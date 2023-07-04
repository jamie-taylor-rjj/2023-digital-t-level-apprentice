namespace InvoiceGen.Services.InvoiceServices;

public class InvoicePager : IPageInvoices
{
    private readonly IMapper<InvoiceViewModel, Invoice> _invoiceViewModelMapper;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoicePager> _logger;

    public InvoicePager(ILogger<InvoicePager> logger,
        IInvoiceRepository invoiceRepository,
        IMapper<InvoiceViewModel, Invoice> invoiceViewModelMapper)
    {
        _logger = logger;
        _invoiceRepository = invoiceRepository;
        _invoiceViewModelMapper = invoiceViewModelMapper;
    }

    public PagedResponse<InvoiceViewModel> GetPage(int pageNumber, int pageSize = 10)
    {
        using (_logger.BeginScope(
                   "{NameOfService} creating paged response of {ViewModelName} with page number of {PageNumber} and page size of {PageSize}",
                   nameof(InvoicePager), nameof(InvoiceViewModel), pageNumber, pageSize))
        {
            var pageNumberToUse = pageNumber < 1
                ? 1
                : pageNumber;

            var records = _invoiceRepository.GetAsQueryable();

            var totalCount = records.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var page = records
                .OrderBy(c => c.ClientId)
                .Skip((pageNumberToUse - 1) * pageSize)
                .Take(pageSize);

            return new PagedResponse<InvoiceViewModel>
            {
                Data = page.AsEnumerable().Select(_invoiceViewModelMapper.Convert).ToList(),
                PageNumber = pageNumberToUse,
                PageSize = page.Count(),
                TotalPages = totalPages,
                TotalRecords = totalCount
            };
        }
    }
}
