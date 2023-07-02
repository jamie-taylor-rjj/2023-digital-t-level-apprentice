namespace InvoiceGen.Services.ClientServices;

public interface IDeleteClients
{
    Task DeleteClient(int clientId);
}
