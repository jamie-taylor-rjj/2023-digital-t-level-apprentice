using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public interface IInvoiceListService
    {
        Task<List<InvoiceModel>> GetInvoices();
    }
}
