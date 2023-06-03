namespace Invoice_Gen.WebApi.Services;

public interface IClientService
{
    List<ClientNameViewModel> GetClients();
    ClientNameViewModel? GetById(int id);
    Task<int> CreateNewClient(ClientCreationModel inputClient);
    Task DeleteClient(int clientId);
}
