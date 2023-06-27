using System;

namespace Invoice_GenUI.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public double VatRate { get; set; }
        public LineItemModel? LineItems { get; set; }
    }
}
