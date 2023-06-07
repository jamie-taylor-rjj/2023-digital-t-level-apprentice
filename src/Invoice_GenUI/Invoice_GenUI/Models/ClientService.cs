using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Invoice_GenUI.ViewModels;

namespace Invoice_GenUI.Models
{
    public interface IClientService // needs method in interface
    {
        Task<List<ClientNameViewModel>> GetClientNames();
        Task<bool> PutClient(string name, string address, string contact, string email);
    }
    public class ClientService : IClientService
    {
        public async Task<List<ClientNameViewModel>> GetClientNames()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = await client.GetAsync("clients"); // http request for base address + clients

                response.EnsureSuccessStatusCode(); // Makes sure response is valid

                return await response.Content.ReadFromJsonAsync<List<ClientNameViewModel>>() ?? new();
            }
        }


        public async Task<bool> PutClient(string name, string address, string contact, string email) // valid values passed from xaml.cs
        {
            bool result = false;

            var clientDetails = new ClientPostModel() // Creating client with the valid values entered by the user in the UI
            {
                ClientName = name,
                ClientAddress = address,
                ContactName = contact,
                ContactEmail = email
            };
            using (var client = new HttpClient())
            {
                // Post request
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/"); // API URL

                var json = JsonSerializer.Serialize(clientDetails); // Turn C# object into json
                var content = new StringContent(json, Encoding.UTF8, "application/json"); // Saying that information im sending comes in json formatting 


                var responseMessage = await client.PutAsync("Clients/Client", content); // Creating the client, choosing the correct endpoint, want to post my content
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
