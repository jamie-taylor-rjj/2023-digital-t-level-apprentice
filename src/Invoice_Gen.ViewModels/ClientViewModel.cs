namespace Invoice_Gen.ViewModels;

public record ClientViewModel
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = null!;
    public string ClientAddress { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string ContactName { get; set; } = null!;
}
