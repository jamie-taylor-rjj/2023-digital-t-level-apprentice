using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.WebApi.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IMapper<InvoiceCreateModel, Invoice> _invoiceCreateModelMapper;
    private readonly IMapper<InvoiceViewModel, Invoice> _invoiceViewModelMapper;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(ILogger<InvoiceService> logger,
        IInvoiceRepository invoiceRepository,
        IMapper<InvoiceViewModel, Invoice> invoiceViewModelMapper,
        IMapper<InvoiceCreateModel, Invoice> invoiceCreateModelMapper)
    {
        _logger = logger;
        _invoiceRepository = invoiceRepository;
        _invoiceViewModelMapper = invoiceViewModelMapper;
        _invoiceCreateModelMapper = invoiceCreateModelMapper;
    }

    public InvoiceViewModel? GetById(int id)
    {
        using (_logger.BeginScope("{InvoiceService} getting invoice record for {ID}", nameof(InvoiceService), id))
        {
            var invoice = _invoiceRepository.GetAll().FirstOrDefault(f => f.InvoiceId == id);

            _logger.LogInformation("Returning {InvoiceViewModel} for {ID}", nameof(InvoiceViewModel), id);
            return invoice == null ? null : _invoiceViewModelMapper.Convert(invoice);
        }
    }

    public async Task<int> CreateNewInvoice(InvoiceCreateModel newInvoice)
    {
        using (_logger.BeginScope("{InvoiceService} creating new invoice record for clientId {ClientId}",
                   nameof(InvoiceService),
                   newInvoice.ClientId))
        {
            var entity = _invoiceCreateModelMapper.Convert(newInvoice);

            // do this better
            foreach (var li in entity.LineItems)
            {
                li.Invoice = entity;
            }

            var response = await _invoiceRepository.Add(entity);
            _logger.LogInformation("Generated ID of new invoice is {InvoiceID}", response.InvoiceId);
            return response.InvoiceId;
        }
    }
}
