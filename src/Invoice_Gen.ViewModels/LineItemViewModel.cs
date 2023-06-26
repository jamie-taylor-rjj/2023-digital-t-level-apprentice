namespace Invoice_Gen.ViewModels;

public class LineItemViewModel
{
    public int LineItemId { get; set; }
    public int InvoiceId { get; set; }
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public double Cost { get; set; }
}
