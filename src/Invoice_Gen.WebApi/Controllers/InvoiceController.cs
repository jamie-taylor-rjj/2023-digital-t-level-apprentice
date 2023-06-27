using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Gen.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    private readonly ILogger<InvoiceController> _logger;

    public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService invoiceService)
    {
        _logger = logger;
        _invoiceService = invoiceService;
    }

    /// <summary>
    /// Gets the instance of <see cref="InvoiceViewModel"/> for the provided <paramref name="invoiceId"/>
    /// </summary>
    /// <returns>
    /// The <see cref="InvoiceViewModel"/> instance which matches the supplied <paramref name="invoiceId"/>
    /// or a <see cref="StatusCodes.Status404NotFound"/> if one cannot be found
    /// </returns>
    [HttpGet("{invoiceId}", Name = "GetInvoiceById")]
    [ProducesResponseType(typeof(ClientViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetInvoiceById(int invoiceId)
    {
        using (_logger.BeginScope("Getting invoice data for {ID}", invoiceId))
        {
            var invoice = _invoiceService.GetById(invoiceId);
            if (invoice == null)
            {
                _logger.LogInformation("Unable to find invoice record");
                return new NotFoundResult();
            }

            _logger.LogInformation("Returning {InvoiceViewModel} for {ID}", nameof(InvoiceViewModel), invoiceId);
            return new OkObjectResult(invoice);
        }
    }

    /// <summary>
    /// Allows API consumers to create new Invoice records in the database using the values of the
    /// fields in the provided <see cref="InvoiceCreateModel"/>
    /// </summary>
    /// <param name="newInvoice">An object which describes the new Client record</param>
    /// <returns>
    /// OK (i.e. 200) if the new record could be created
    /// Internal Server Error (i.e. 500) if the record could not be created
    /// </returns>
    [HttpPut]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateClient(InvoiceCreateModel newInvoice)
    {
        using (_logger.BeginScope("Request to create new client Invoice for client {ClientId} received",
                   newInvoice.ClientId))
        {
            var response = await _invoiceService.CreateNewInvoice(newInvoice);
            if (response == default)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new CreatedResult(nameof(GetInvoiceById), new { clientId = response });
        }
    }
}
