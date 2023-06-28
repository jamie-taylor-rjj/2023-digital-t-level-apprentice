using System;
using System.Collections.ObjectModel;

namespace Invoice_GenUI.Models
{
    public class InvoiceModel
    {
        public int ClientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public double VatRate { get; set; }
        public ObservableCollection<LineItemModel> LineItems { get; set; }
    }
}
