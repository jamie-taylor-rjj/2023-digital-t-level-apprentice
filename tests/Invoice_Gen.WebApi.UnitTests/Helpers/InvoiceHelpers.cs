namespace Invoice_Gen.WebApi.UnitTests.Helpers;

public static class InvoiceHelpers
{
    private static readonly Random Rng = new();

    public static List<Invoice> GenerateRandomListOfInvoices(int length)
    {
        var retVal = new List<Invoice>();
        var count = 0;
        while (count < length)
        {
            retVal.Add(new Invoice
            {
                InvoiceId = Rng.Next(1,
                    200),
                ClientId = Rng.Next(1,
                    200),
                IssueDate = default,
                DueDate = default,
                VatRate = Rng.Next(10, 25),
                LineItems = new List<LineItem>(),

            });
            count++;
        }

        return retVal;
    }
}
