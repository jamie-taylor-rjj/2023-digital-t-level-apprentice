using Microsoft.AspNetCore.Mvc;

namespace Invoice_Gen.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ClientsController : ControllerBase
{
    private ILogger<ClientsController> _logger;
    private readonly IClientService _clientService;

    public ClientsController(ILogger<ClientsController> logger, IClientService clientService)
    {
        _logger = logger;
        _clientService = clientService;
    }

    /// <summary>
    /// Returns a new instance of <see cref="ClientNameViewModel"/>
    /// </summary>
    /// <returns>
    /// A list of <see cref="ClientNameViewModel"/> instances with some default data
    /// </returns>
    [HttpGet(Name = "GetClients")]
    [ProducesResponseType(typeof(List<ClientNameViewModel>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var clients = _clientService.GetClients();
        return new OkObjectResult(clients);
    }
    
    /// <summary>
    /// Gets the instance of <see cref="ClientNameViewModel"/> for the provided <paramref name="clientId"/>
    /// </summary>
    /// <returns>
    /// A list of <see cref="ClientNameViewModel"/> instances with some default data
    /// </returns>
    [HttpGet("{clientId}", Name="GetClientById")]
    [ProducesResponseType(typeof(ClientNameViewModel), StatusCodes.Status200OK)]
    public IActionResult GetClientById(int clientId)
    {
        var client = _clientService.GetById(clientId);
        if (client == null)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(client);
    }
}
