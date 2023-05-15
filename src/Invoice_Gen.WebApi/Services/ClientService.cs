using Invoice_Gen.WebApi.Models;
using Bogus;

namespace Invoice_Gen.WebApi.Services;

public class ClientService : IClientService
{
    public List<Client> GetClients()
    {
        var clientIds = 1;
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
