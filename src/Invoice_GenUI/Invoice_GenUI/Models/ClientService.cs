using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Invoice_GenUI.ViewModels;

namespace Invoice_GenUI.Models
{
    public interface IClientService
    {
        List<ClientNameViewModel> GetClientNames();
    }
    public class ClientService : IClientService
    {
        public List<ClientNameViewModel> GetClientNames()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = client.GetAsync("clients").Result; // http request for base address + clients

                response.EnsureSuccessStatusCode(); // Makes sure response is valid

                return response.Content.ReadFromJsonAsync<List<ClientNameViewModel>>().Result;
            }
        }
    }
}
