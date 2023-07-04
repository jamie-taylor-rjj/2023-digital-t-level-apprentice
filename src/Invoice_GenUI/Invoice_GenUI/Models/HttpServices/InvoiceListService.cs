using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public class InvoiceListService : BaseService, IInvoiceListService
    {
        public async Task<List<InvoiceModel>> GetInvoices()
        {
            return await SendHttpGetRequest<List<InvoiceModel>>("Invoices") ?? new();
        }
        public async Task<bool> DeleteInvoice(int id)
        {
            return await SendHttpDeleteRequest($"Invoices/{id}");
        }
        public async Task<InvoicePageModel> GetInvoicePages(int pageNumber, int pageSize)
        {
            return await SendHttpGetRequest<InvoicePageModel>($"Invoices/page/{pageNumber}?pageSize={pageSize}") ?? new();
        }
    }
}
