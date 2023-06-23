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
        public async Task<bool> PutClient(CreateClientModel newClient) // valid values passed from xaml.cs
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                // Post request
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/"); // API URL

                var json = JsonSerializer.Serialize(newClient); // Turn C# object into json
                var content = new StringContent(json, Encoding.UTF8, "application/json"); // Saying that information im sending comes in json formatting 


                var responseMessage = await client.PutAsync("Clients/Client", content); // Creating the client, choosing the correct endpoint, want to put my content
                if (responseMessage.IsSuccessStatusCode) // Makes sure response is valid
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync(); // Wait until get result
                    result = true;
                }

                return result;
            }
        }
    }
}
