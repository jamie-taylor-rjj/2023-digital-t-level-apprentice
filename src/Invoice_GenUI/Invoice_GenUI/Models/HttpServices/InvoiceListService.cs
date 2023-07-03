using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public class InvoiceListService : BaseService, IInvoiceListService
    {
        public async Task<List<InvoiceModel>> GetInvoices()
        {
            return await SendHttpGetRequest<List<InvoiceModel>>("Invoice") ?? new();
        }
        public async Task<bool> DeleteInvoice(int id)
        {
            return await SendHttpDeleteRequest($"Invoice/{id}");
        }
    }
}
