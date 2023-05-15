using App.WebApi.Models;
using Bogus;

namespace App.WebApi.Services;

public class ClientService : IClientService
{
    public List<Client> GetClients()
    {
        var clientIds = 0;
        var clientsFaker = new Faker<Client>()
            .CustomInstantiator(f => new Client(clientIds++, f.Company.CompanyName()))
            .FinishWith((f, u) => { });
        return new List<Client>()
        {
            clientsFaker.Generate(),
            clientsFaker.Generate(),
            clientsFaker.Generate(),
            clientsFaker.Generate(),
            clientsFaker.Generate(),
        };
    }
}
