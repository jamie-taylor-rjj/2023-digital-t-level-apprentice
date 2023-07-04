﻿using System.Net.Mime;
using InvoiceGen.Services.ClientServices;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Gen.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ClientsController : ControllerBase
{
    private readonly ILogger<ClientsController> _logger;
    private readonly IGetClients _clientGetter;
    private readonly ICreateClients _clientCreator;
    private readonly IDeleteClients _clientDeleter;
    private readonly IPageClients _pageClients;

    public ClientsController(ILogger<ClientsController> logger, IGetClients clientGetter, IPageClients pageClients,
        ICreateClients clientCreator, IDeleteClients clientDeleter)
    {
        _logger = logger;
        _clientGetter = clientGetter;
        _pageClients = pageClients;
        _clientCreator = clientCreator;
        _clientDeleter = clientDeleter;
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
            var clients = _clientGetter.GetClients();

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
            var client = _clientGetter.GetById(clientId);
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
    /// Used to get a PAGED list of <see cref="ClientViewModel"/> instances, using the <paramref name="pageNumber"/>
    /// and <paramref name="pageSize"/> parameters as filters for the paged list
    /// </summary>
    /// <param name="pageNumber" example="1">The page number requested; MUST be a positive integer</param>
    /// <param name="pageSize" example="10">The number of items to return per page. MUST be either 10, 25, or 50</param>
    /// <returns>
    /// A new instance of the <see cref="PagedResponse{T}"/> where T is a <see cref="ClientViewModel" />
    /// with the requested number of items (if available) and data about how many pages are available
    /// </returns>
    [ProducesResponseType(typeof(PagedResponse<ClientViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("page/{pageNumber}", Name = "GetPageOfClients")]
    public IActionResult GetPage(int pageNumber, [FromQuery] int pageSize = 10)
    {
        using (_logger.BeginScope("Getting page {PageNumber} of Clients; requested {PageSize} per page",
                   pageNumber, pageSize))
        {
            if (pageNumber <= 0)
            {
                _logger.LogInformation("Bad value supplied for page number: {PageNumber}", pageNumber);
                return new NotFoundResult();
            }

            switch (pageSize)
            {
                case 10:
                case 25:
                case 50:
                    break;
                default:
                    _logger.LogInformation("Bad value supplied for pageSize: {PageSize}", pageSize);
                    return new BadRequestResult();
            }

            var pagedContent = _pageClients.GetPage(pageNumber, pageSize);
            return new OkObjectResult(pagedContent);
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
    [HttpPut]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateClient(ClientCreationModel inputClient)
    {
        using (_logger.BeginScope("Request to create new client {ClientName} received", inputClient.ClientName))
        {
            var response = await _clientCreator.CreateNewClient(inputClient);

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
    [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteClient(int clientId)
    {
        using (_logger.BeginScope("Request to delete client {ClientId} received", clientId))
        {
            if (clientId <= (int)default!)
            {
                _logger.LogInformation("Supplied ClientId was 0");
                return new BadRequestResult();
            }

            await _clientDeleter.DeleteClient(clientId);

            return new OkResult();
        }
    }
}
