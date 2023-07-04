using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public partial class InvoiceService : BaseService, IInvoiceService
    {
        public async Task<bool> PutInvoice(InvoiceModel newInvoice)
        {
            return await SendHttpPutRequest("Invoices", newInvoice);
        }
        public async Task<InvoiceModel> GetSingleInvoiceDetails(int id)
        {
            return await SendHttpGetRequest<InvoiceModel>($"Invoices/{id}") ?? new();
        }
    }
}
