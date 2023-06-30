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
        public async Task<List<ClientNameModel>> GetClientNames()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = await client.GetAsync("clients");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<ClientNameModel>>() ?? new();
            }
        }
        public async Task<List<CreateClientModel>> GetClientDetails()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = await client.GetAsync("clients");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<CreateClientModel>>() ?? new();
            }
        }
        public async Task<CreateClientModel> GetSingleClientDetails(int index)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = await client.GetAsync($"clients/{index}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CreateClientModel>() ?? new();
            }
        }
        public async Task<bool> PutClient(CreateClientModel newClient)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var json = JsonSerializer.Serialize(newClient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var responseMessage = await client.PutAsync("Clients/Client", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    result = true;
                }

                return result;
            }
        }
        public async Task<bool> DeleteClient(int id)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var respone = await client.DeleteAsync($"Clients/{id}");

                respone.EnsureSuccessStatusCode();

                if (respone.IsSuccessStatusCode)
                {
                    result = true;
                }
                return result;
            }
        }
    }
}
