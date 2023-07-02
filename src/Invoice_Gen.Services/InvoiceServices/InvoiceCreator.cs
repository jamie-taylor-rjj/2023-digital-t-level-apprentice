namespace InvoiceGen.Services.InvoiceServices;

public class InvoiceCreator : ICreateInvoices
{
    private readonly IMapper<InvoiceCreateModel, Invoice> _invoiceCreateModelMapper;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoiceCreator> _logger;

    public InvoiceCreator(ILogger<InvoiceCreator> logger,
        IInvoiceRepository invoiceRepository,
        IMapper<InvoiceCreateModel, Invoice> invoiceCreateModelMapper)
    {
        _logger = logger;
        _invoiceRepository = invoiceRepository;
        _invoiceCreateModelMapper = invoiceCreateModelMapper;
    }
    
    public async Task<int> CreateNewInvoice(InvoiceCreateModel newInvoice)
    {
        using (_logger.BeginScope("{InvoiceService} creating new invoice record for clientId {ClientId}",
                   nameof(InvoicePager),
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
