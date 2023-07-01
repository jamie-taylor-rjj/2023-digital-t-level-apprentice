namespace Invoice_Gen.ViewModels;

public record ClientViewModel : IPageable
{
    public int ClientId { get; init; }
    public string ClientName { get; init; } = null!;
    public string ClientAddress { get; init; } = null!;
    public string ContactEmail { get; init; } = null!;
    public string ContactName { get; init; } = null!;
}
