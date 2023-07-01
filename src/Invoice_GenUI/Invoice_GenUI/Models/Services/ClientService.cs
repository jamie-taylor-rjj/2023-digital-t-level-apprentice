using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            bool result = false;

            var json = JsonSerializer.Serialize(newClient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await CreateHttpClient().PutAsync("Clients/Client", content);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }

            return result;
        }
        public async Task<bool> DeleteClient(int id)
        {
            bool result = false;

            var response = await CreateHttpClient().DeleteAsync($"Clients/{id}");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }

            return result;
        }
    }
}
