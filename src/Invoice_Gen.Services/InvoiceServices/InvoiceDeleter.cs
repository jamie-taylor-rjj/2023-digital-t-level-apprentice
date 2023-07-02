namespace InvoiceGen.Services.InvoiceServices;

public class InvoiceDeleter : IDeleteInvoices
{
    private readonly ILogger<InvoiceDeleter> _logger;
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceDeleter(ILogger<InvoiceDeleter> logger, IInvoiceRepository invoiceRepository)
    {
        _logger = logger;
        _invoiceRepository = invoiceRepository;
    }

    public async Task DeleteInvoice(int invoiceId)
    {
        _logger.LogInformation("Deleting invoice with ID of {InvoiceId}", invoiceId);
        await _invoiceRepository.Delete(invoiceId);
    }
}
