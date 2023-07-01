using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public partial class InvoiceService : BaseService, IInvoiceService
    {
        public async Task<bool> PutInvoice(InvoiceModel newInvoice)
        {
            bool result = false;

            var json = JsonSerializer.Serialize(newInvoice);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await CreateHttpClient().PutAsync("Invoice", content);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }

            return result;
        }
        public async Task<InvoiceModel> GetSingleInvoiceDetails(int id)
        {
            return await SendHttpGetRequest<InvoiceModel>($"Invoice/{id}") ?? new();
        }
    }
}
