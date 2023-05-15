using App.WebApi.Models;

namespace App.WebApi.Services;

public interface IClientService
{
    List<Client> GetClients();
}
