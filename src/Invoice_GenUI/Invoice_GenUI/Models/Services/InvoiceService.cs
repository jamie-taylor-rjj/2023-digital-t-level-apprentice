using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public partial class InvoiceService : IInvoiceService
    {
        public async Task<bool> PutInvoice(InvoiceModel newInvoice)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var json = JsonSerializer.Serialize(newInvoice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PutAsync("Invoice", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    result = true;
                }

                return result;
            }
        }
        public async Task<InvoiceModel> GetSingleInvoiceDetails(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = await client.GetAsync($"Invoice/{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<InvoiceModel>() ?? new();
            }
        }
    }
}
