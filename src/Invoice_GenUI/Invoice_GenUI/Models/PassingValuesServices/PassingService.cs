using System.Collections.ObjectModel;

namespace Invoice_GenUI.Models.PassingValuesServices
{
    public class PassingService : IPassingService
    {
        public int InvoiceID { get; set; }
        public int ClientID { get; set; }
        public ObservableCollection<LineItemModel>? StoredItems { get; set; } = new ObservableCollection<LineItemModel>();
    }
}
