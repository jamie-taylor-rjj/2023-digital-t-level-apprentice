namespace Invoice_Gen.WebApi.Services;

public interface IClientService
{
    List<ClientViewModel> GetClients();
    ClientViewModel? GetById(int id);
    PagedResponse<ClientViewModel> GetPage(int pageNumber, int pageSize = 10);
    Task<int> CreateNewClient(ClientCreationModel inputClient);
    Task DeleteClient(int clientId);
}
