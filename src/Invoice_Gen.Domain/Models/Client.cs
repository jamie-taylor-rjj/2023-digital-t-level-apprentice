using System.Diagnostics.CodeAnalysis;

namespace Invoice_Gen.Domain.Models;

[ExcludeFromCodeCoverage]
public class Client
{
    public int ClientId { get; init; }
    public string ClientName { get; init; } = null!;
    public string ClientAddress { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
}
