namespace Invoice_Gen.WebApi.Services;

public interface IClientService
{
    List<ClientViewModel> GetClients();
    ClientViewModel? GetById(int id);
    Task<int> CreateNewClient(ClientCreationModel inputClient);
    Task DeleteClient(int clientId);
}
