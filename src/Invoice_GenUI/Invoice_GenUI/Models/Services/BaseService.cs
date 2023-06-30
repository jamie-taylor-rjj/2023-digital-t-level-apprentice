using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public class BaseService
    {
        public HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");
            return client;
        }
        public async Task<T?> SendHttpRequest<T>(string url) // T can be nullable
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
        }
    }
}
