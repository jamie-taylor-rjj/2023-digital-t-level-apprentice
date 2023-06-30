using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public partial class ClientService : IClientService
    {
        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");
            return client;
        }
        private async Task<T?> SendHttpRequest<T>(string url) // T can be nullable
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
        }
        public async Task<List<ClientNameModel>> GetClientNames()
        {
            return await SendHttpRequest<List<ClientNameModel>>("Clients") ?? new();
        }
        public async Task<List<CreateClientModel>> GetClientDetails()
        {
            return await SendHttpRequest<List<CreateClientModel>>("Clients") ?? new();
        }
        public async Task<CreateClientModel> GetSingleClientDetails(int id)
        {
            return await SendHttpRequest<CreateClientModel>($"Clients/{id}") ?? new();
        }
        public async Task<bool> PutClient(CreateClientModel newClient)
        {
            bool result = false;

            using (var client = CreateHttpClient())
            {
                var json = JsonSerializer.Serialize(newClient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("Clients/Client", content);

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            return result;
        }
        public async Task<bool> DeleteClient(int id)
        {
            bool result = false;

            using(var client = CreateHttpClient())
            {
                var response = await client.DeleteAsync($"Clients/{id}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode )
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
