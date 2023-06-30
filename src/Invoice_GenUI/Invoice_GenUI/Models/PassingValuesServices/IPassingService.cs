using System.Collections.ObjectModel;

namespace Invoice_GenUI.Models.PassingValuesServices
{
    public interface IPassingService
    {
        int InvoiceID { get; set; }
        int ClientID { get; set; }
        ObservableCollection<LineItemModel>? StoredItems { get; set; }
    }
}
