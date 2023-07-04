namespace InvoiceGen.Services.ClientServices;

public interface ICreateClients
{
    Task<int> CreateNewClient(ClientCreationModel inputClient);
}
