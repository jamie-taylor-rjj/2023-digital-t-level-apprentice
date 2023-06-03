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
    [HttpGet("{clientId}", Name = "GetClientById")]
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

    /// <summary>
    /// Allows API consumers to create new Client records in the database using the values of the
    /// fields in the provided <see cref="ClientCreationModel"/>
    /// </summary>
    /// <param name="inputClient">An object which describes the new Client record</param>
    /// <returns>
    /// OK (i.e. 200) if the new record could be created
    /// Internal Server Error (i.e. 500) if the record could not be created
    /// </returns>
    [HttpPut("Client")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateClient(ClientCreationModel inputClient)
    {
        var response = await _clientService.CreateNewClient(inputClient);
        if (response == default)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new CreatedResult(nameof(GetClientById), new { clientId = response });
    }

    /// <summary>
    /// Used to delete a Client record from the database. The Client record selected for
    /// deletion is the one which matches on <paramref name="clientId"/>
    /// </summary>
    /// <param name="clientId">The ID of the client record to delete</param>
    /// <returns>
    /// OK (i.e. 200) if the new record could be deleted
    /// Bad Request (i.e. 400) if clientId is incorrect
    /// </returns>
    [HttpDelete("{clientId}", Name = "Client")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteClient(int clientId)
    {
        if (clientId == default)
        {
            return new BadRequestResult();
        }

        await _clientService.DeleteClient(clientId);

        return new OkResult();
    }
}
