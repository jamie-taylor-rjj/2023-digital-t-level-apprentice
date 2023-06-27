using System.Diagnostics.CodeAnalysis;

namespace Invoice_Gen.Domain.Models;

[ExcludeFromCodeCoverage]
public class Invoice
{
    public int InvoiceId { get; init; }
    public int ClientId { get; init; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public int VatRate { get; set; }
    public Client Client { get; init; } = null!;
    public IList<LineItem> LineItems { get; set; } = new List<LineItem>();
}
