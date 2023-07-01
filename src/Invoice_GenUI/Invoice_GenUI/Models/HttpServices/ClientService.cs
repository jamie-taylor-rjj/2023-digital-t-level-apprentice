using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public partial class ClientService : BaseService, IClientService
    {
        public async Task<List<ClientNameModel>> GetClientNames()
        {
            return await SendHttpGetRequest<List<ClientNameModel>>("Clients") ?? new();
        }
        public async Task<List<CreateClientModel>> GetClientDetails()
        {
            return await SendHttpGetRequest<List<CreateClientModel>>("Clients") ?? new();
        }
        public async Task<CreateClientModel> GetSingleClientDetails(int id)
        {
            return await SendHttpGetRequest<CreateClientModel>($"Clients/{id}") ?? new();
        }
        public async Task<bool> PutClient(CreateClientModel newClient)
        {
            return await SendHttpPutRequest("Clients/Client", newClient);
        }
        public async Task<bool> DeleteClient(int id)
        {
            return await SendHttpDeleteRequest($"Clients/{id}");
        }
    }
}
