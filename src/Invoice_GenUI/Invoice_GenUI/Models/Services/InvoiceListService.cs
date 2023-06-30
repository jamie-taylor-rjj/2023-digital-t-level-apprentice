﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
        public async Task<bool> DeleteInvoice(int id)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var respone = await client.DeleteAsync($"Invoice/{id}");

                respone.EnsureSuccessStatusCode();

                if (respone.IsSuccessStatusCode)
                {
                    result = true;
                }
                return result;
            }
        }
    }
}