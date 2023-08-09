namespace Invoice_Gen.WebApi.UnitTests.Helpers;

public static class ClientHelpers
{
    private static readonly Random Rng = new();

    public static List<Client> GenerateRandomListOfClients(int length)
    {
        var retVal = new List<Client>();
        var count = 0;
        while (count < length)
        {
            retVal.Add(new Client
            {
                ClientName = Guid.NewGuid().ToString(),
                ClientAddress = Guid.NewGuid().ToString(),
                ContactName = Guid.NewGuid().ToString(),
                ContactEmail = Guid.NewGuid().ToString(),
            });
            count++;
        }

        return retVal;
    }

}
