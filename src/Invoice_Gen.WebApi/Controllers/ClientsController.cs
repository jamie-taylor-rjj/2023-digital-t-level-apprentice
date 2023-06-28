using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Gen.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ClientsController : ControllerBase
{
    private readonly ILogger<ClientsController> _logger;
    private readonly IClientService _clientService;

    public ClientsController(ILogger<ClientsController> logger, IClientService clientService)
    {
        _logger = logger;
        _clientService = clientService;
    }

    /// <summary>
    /// Returns all clients in the system in a list of <see cref="ClientViewModel"/>
    /// </summary>
    /// <returns>
    /// A list of <see cref="ClientViewModel"/> instances with some default data
    /// </returns>
    [HttpGet(Name = "GetClients")]
    [ProducesResponseType(typeof(List<ClientViewModel>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        using (_logger.BeginScope("Getting all clients"))
        {
            var clients = _clientService.GetClients();

            _logger.LogInformation("Returning list of {ClientNameViewModel}", typeof(ClientViewModel));
            return new OkObjectResult(clients);
        }
    }

    /// <summary>
    /// Gets the instance of <see cref="ClientViewModel"/> for the provided <paramref name="clientId"/>
    /// </summary>
    /// <returns>
    /// A list of <see cref="ClientViewModel"/> instances with some default data
    /// </returns>
    [HttpGet("{clientId}", Name = "GetClientById")]
    [ProducesResponseType(typeof(ClientViewModel), StatusCodes.Status200OK)]
    public IActionResult GetClientById(int clientId)
    {
        using (_logger.BeginScope("Getting client data for {ID}", clientId))
        {
            var client = _clientService.GetById(clientId);
            if (client == null)
            {
                _logger.LogInformation("Unable to find client record");
                return new NotFoundResult();
            }

            _logger.LogInformation("Returning {ClientNameViewModel} for {ID}", nameof(ClientViewModel), clientId);
            return new OkObjectResult(client);
        }
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
        using (_logger.BeginScope("Request to create new client {ClientName} received", inputClient.ClientName))
        {
            var response = await _clientService.CreateNewClient(inputClient);
            if (response == default)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new CreatedResult(nameof(GetClientById), new { clientId = response });
        }
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
        using (_logger.BeginScope("Request to delete client {ClientId} received", clientId))
        {
            if (clientId == default)
            {
                _logger.LogInformation("Supplied ClientId was 0");
                return new BadRequestResult();
            }

            await _clientService.DeleteClient(clientId);

            return new OkResult();
        }
    }
}
