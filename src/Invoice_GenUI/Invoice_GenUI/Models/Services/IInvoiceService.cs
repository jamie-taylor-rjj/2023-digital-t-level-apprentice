using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public interface IInvoiceService
    {
        Task<bool> PutInvoice(InvoiceModel newInvoice);
        Task<InvoiceModel> GetSingleInvoiceDetails(int id);
    }
}
