namespace Invoice_Gen.ViewModels;

public class HelloResponse
{
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public string Message { get; init; } = "Hello there!";
}
