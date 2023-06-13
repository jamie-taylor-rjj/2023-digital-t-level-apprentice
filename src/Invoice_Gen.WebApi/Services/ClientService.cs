﻿using Invoice_Gen.Domain.Models;

namespace Invoice_Gen.WebApi.Services;

public class ClientService : IClientService
{
    private readonly IMapper<ClientViewModel, Client> _clientViewModelMapper;
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientService> _logger;
    public ClientService(IMapper<ClientViewModel, Client> clientViewModelMapper,
        IClientRepository clientRepository, ILogger<ClientService> logger)
    {
        _clientViewModelMapper = clientViewModelMapper;
        _clientRepository = clientRepository;
        _logger = logger;
    }
    public List<ClientViewModel> GetClients()
    {
        _logger.BeginScope("{ClientService} getting all clients", nameof(ClientService));
        var all = _clientRepository.GetAll();

        _logger.LogInformation("Retrieved {Count} {ClientModel}", all.Count, nameof(Client));

        _logger.LogInformation("Converting to List of {ClientNameViewModel} using {Mapper}", nameof(ClientViewModel), typeof(ClientNameViewModelMapper));
        var returnData = all.Select(c => _clientViewModelMapper.Convert(c)).ToList();

        _logger.LogInformation("Returning {count} of {ClientNameViewModel} instances", returnData.Count(), nameof(ClientViewModel));
        return returnData;
    }

    public ClientViewModel? GetById(int id)
    {
        _logger.BeginScope("{ClientService} getting client record for {ID}", nameof(ClientService), id);

        var client = _clientRepository.GetAll().FirstOrDefault(f => f.ClientId == id);

        _logger.LogInformation("Returning {ClientNameViewModel} for {ID}", nameof(ClientViewModel), id);
        return client == null ? null : _clientViewModelMapper.Convert(client);
    }

    public async Task<int> CreateNewClient(ClientCreationModel inputClient)
    {
        _logger.BeginScope("{ClientService} creating new client record for {ClientName}", nameof(ClientService), inputClient.ClientName);
        var response = await _clientRepository.Add(new Client
        {
            ClientAddress = inputClient.ClientAddress,
            ClientName = inputClient.ClientName,
            ContactEmail = inputClient.ContactEmail,
            ContactName = inputClient.ContactName
        });
        _logger.LogInformation("Generated ID of new client is {ClientId}", response.ClientId);
        return response.ClientId;
    }

    public async Task DeleteClient(int clientId)
    {
        await _clientRepository.Delete(clientId);
    }
}
