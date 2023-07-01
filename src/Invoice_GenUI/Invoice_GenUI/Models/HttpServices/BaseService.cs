using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

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
        public async Task<T?> SendHttpGetRequest<T>(string url)
        {
                var response = await CreateHttpClient().GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<bool> SendHttpDeleteRequest(string url)
        {
            bool result = false;
            var response = await CreateHttpClient().DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }
        public async Task<bool> SendHttpPutRequest<T>(string url, T model)
        {
            bool result = false;
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await CreateHttpClient().PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }
    }
}
