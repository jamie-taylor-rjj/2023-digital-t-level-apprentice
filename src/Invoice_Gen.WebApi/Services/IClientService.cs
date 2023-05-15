using Invoice_Gen.WebApi.Models;

namespace Invoice_Gen.WebApi.Services;

public interface IClientService
{
    List<Client> GetClients();
}
