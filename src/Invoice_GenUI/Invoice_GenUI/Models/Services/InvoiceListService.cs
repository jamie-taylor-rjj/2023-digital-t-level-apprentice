using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;

namespace Invoice_GenUI.Models.Services
{
    public class InvoiceListService : IInvoiceListService
    {
        public async Task<List<InvoiceModel>> GetInvoices()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = await client.GetAsync("Invoice");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<InvoiceModel>>() ?? new();
            }
        }
    }
}
