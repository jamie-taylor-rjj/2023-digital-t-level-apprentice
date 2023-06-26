using System.Diagnostics.CodeAnalysis;

namespace Invoice_Gen.Domain.Models;

[ExcludeFromCodeCoverage]
public class LineItem
{
    public int LineItemId { get; set; }
    public int InvoiceId { get; set; }
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public double Cost { get; set; }
    public Invoice Invoice { get; set; } = null!;
}
