namespace Invoice_Gen.ViewModels;

public record InvoiceViewModel : IPageable
{
    public int InvoiceId { get; set; }
    public int ClientId { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime IssueDate { get; set; }
    public int VatRate { get; set; }
    public List<LineItemViewModel> LineItems { get; set; } = new();
}
