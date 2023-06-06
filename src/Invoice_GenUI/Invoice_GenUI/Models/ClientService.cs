using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Invoice_GenUI.ViewModels;

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

        public async Task<bool> EnterClient(string name, string address, string contact, string email) // valid values passed from xaml.cs
        {
            bool result = false;

            var clientURL = "https://2023-invoice-gen.azurewebsites.net/Client"; // API URL

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
                var responseMessage = await client.PostAsJsonAsync(clientURL, clientDetails); // Creating the client
                if(responseMessage.IsSuccessStatusCode) // Makes sure response is valid
                {
                    result = true;
                }
               // var content = await responseMessage.Content.ReadAsStringAsync(); // String of the message

                // Put request


                return result;


            }
        }
    }
}
