using System.Diagnostics.CodeAnalysis;

namespace Invoice_Gen.ViewModels;

[ExcludeFromCodeCoverage]
public class VersionResponse
{
    public string VersionNumber { get; init; } = "0.0.0.0";
    public string AppName { get; init; } = "King Foo";
}
