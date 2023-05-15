using Invoice_Gen.ViewModels;

namespace Invoice_Gen.WebApi.Services;

public interface IClientService
{
    List<ClientNameViewModel> GetClients();
    ClientNameViewModel? GetById(int id);
}
