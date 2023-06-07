using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

        public async Task PutRequest()
        {
            using (var client = new HttpClient())
            {

            }
        }
    }
}
