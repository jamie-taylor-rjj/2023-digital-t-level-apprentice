using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Invoice_GenUI.ViewModels;
using System.Text;
using System.Text.Json;

namespace Invoice_GenUI.Models
{
    public interface IClientService
    {
        Task<List<ClientNameViewModel>> GetClientNames();
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
       

        public async Task<bool> PostClient(string name, string address, string contact, string email) // valid values passed from xaml.cs
        {
            bool result = false;

            var clientDetails = new CreateClientViewModel() // Creating client with the valid values entered by the user in the UI
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
                var responseMessage = await client.PostAsync("Client/Clients", content); // Creating the client, choosing the correct endpoint, want to post my content
                if (responseMessage.IsSuccessStatusCode) // Makes sure response is valid
                {
                    var responseContent = responseMessage.Content.ReadAsStringAsync().Result; // Wait until get result
                    MessageBox.Show(responseContent); // Show result
                    result = true;
                } 

                return result;
            }
        }
    }
}
