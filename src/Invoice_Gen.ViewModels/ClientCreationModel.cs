namespace Invoice_Gen.ViewModels;

public class ClientCreationModel
{
    public string ClientName { get; init; } = null!;
    public string ClientAddress { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
}
