namespace Invoice_Gen.ViewModels;

public class InvoiceCreateModel
{
    public int ClientId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public int VatRate { get; set; }
    public IList<LineItemViewModel> LineItems { get; set; } = new List<LineItemViewModel>();
}
