using Invoice_Gen.WebApi.Models;
using Bogus;

namespace Invoice_Gen.WebApi.Services;

public class ClientService : IClientService
{
    private readonly List<Client> _clients;
    public ClientService()
    {
        var clientIds = 1;
        var clientsFaker = new Faker<Client>()
            .CustomInstantiator(f => new Client(clientIds++, f.Company.CompanyName()))
            .FinishWith((f, u) => { });
        _clients = new List<Client>()
        {
            clientsFaker.Generate(),
            clientsFaker.Generate(),
            clientsFaker.Generate(),
            clientsFaker.Generate(),
            clientsFaker.Generate(),
        };
    }
    public List<Client> GetClients()
    {
        return _clients;
    }

    public Client? GetById(int id)
    {
        var client = _clients.FirstOrDefault(f => f.ClientId == id);
        return client;
    }
}
