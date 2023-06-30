using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public class InvoiceListService : BaseService, IInvoiceListService
    {
        public async Task<List<InvoiceModel>> GetInvoices()
        {
            return await SendHttpRequest<List<InvoiceModel>>("Invoice") ?? new();
        }
        public async Task<bool> DeleteInvoice(int id)
        {
            bool result = false;
            using (var client = CreateHttpClient())
            {
                var response = await client.DeleteAsync($"Invoice/{id}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
