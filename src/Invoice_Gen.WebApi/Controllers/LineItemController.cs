using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Gen.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class LineItemController : ControllerBase
{
    private readonly ILineItemService _lineItemService;
    private readonly ILogger<LineItemController> _logger;

    public LineItemController(ILineItemService lineItemService, ILogger<LineItemController> logger)
    {
        _lineItemService = lineItemService;
        _logger = logger;
    }

    /// <summary>
    /// Gets the instance of <see cref="LineItemViewModel"/> for the provided <paramref name="lineItemId"/>
    /// </summary>
    /// <returns>
    /// The <see cref="LineItemViewModel"/> instance which matches the supplied <paramref name="lineItemId"/>
    /// or a <see cref="StatusCodes.Status404NotFound"/> if one cannot be found
    /// </returns>
    [HttpGet("{lineItemId}", Name = "GetLineItemById")]
    [ProducesResponseType(typeof(ClientViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetInvoiceById(int lineItemId)
    {
        using (_logger.BeginScope("Getting line item data for {ID}", lineItemId))
        {
            var client = _lineItemService.GetById(lineItemId);
            if (client == null)
            {
                _logger.LogInformation("Unable to find line item record");
                return new NotFoundResult();
            }

            _logger.LogInformation("Returning {LineItemViewModel} for {ID}", nameof(LineItemViewModel), lineItemId);
            return new OkObjectResult(client);
        }
    }
}
