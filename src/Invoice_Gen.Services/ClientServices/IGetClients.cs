namespace InvoiceGen.Services.ClientServices;

public interface IGetClients
{
    List<ClientViewModel> GetClients();
    ClientViewModel? GetById(int id);
}
