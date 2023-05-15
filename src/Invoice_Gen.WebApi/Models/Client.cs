namespace Invoice_Gen.WebApi.Models;

public class Client
{
    public Client(int clientId, string clientName)
    {
        ClientId = clientId;
        ClientName = clientName;
    }
    public int ClientId { get; set; }
    public string ClientName { get; set; }
}
