using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text.Json;

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
    }
}
