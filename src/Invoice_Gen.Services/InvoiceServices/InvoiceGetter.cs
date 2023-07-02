namespace InvoiceGen.Services.InvoiceServices;

public class InvoiceGetter : IGetInvoices
{
    private readonly IMapper<InvoiceViewModel, Invoice> _invoiceViewModelMapper;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoiceGetter> _logger;

    public InvoiceGetter(ILogger<InvoiceGetter> logger,
        IInvoiceRepository invoiceRepository,
        IMapper<InvoiceViewModel, Invoice> invoiceViewModelMapper)
    {
        _logger = logger;
        _invoiceRepository = invoiceRepository;
        _invoiceViewModelMapper = invoiceViewModelMapper;
    }

    public List<InvoiceViewModel> GetInvoices()
    {
        using (_logger.BeginScope("{InvoiceService} getting all clients", nameof(InvoicePager)))
        {
            var all = _invoiceRepository.GetAll();

            _logger.LogInformation("Retrieved {Count} {InvoiceModel}", all.Count, nameof(Invoice));

            _logger.LogInformation("Converting to List of {InvoiceViewModel} using {Mapper}",
                nameof(InvoiceViewModel), typeof(InvoiceViewModelMapper));
            var returnData = all.Select(c => _invoiceViewModelMapper.Convert(c)).ToList();

            _logger.LogInformation("Returning {count} of {InvoiceViewModel} instances", returnData.Count,
                nameof(InvoiceViewModel));
            return returnData;
        }
    }

    public InvoiceViewModel? GetById(int id)
    {
        using (_logger.BeginScope("{InvoiceService} getting invoice record for {ID}", nameof(InvoicePager), id))
        {
            var invoice = _invoiceRepository.GetAsQueryable().FirstOrDefault(f => f.InvoiceId == id);

            _logger.LogInformation("Returning {InvoiceViewModel} for {ID}", nameof(InvoiceViewModel), id);
            return invoice == null ? null : _invoiceViewModelMapper.Convert(invoice);
        }
    }
}
