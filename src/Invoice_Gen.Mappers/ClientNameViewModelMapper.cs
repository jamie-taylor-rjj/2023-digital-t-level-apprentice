using Invoice_Gen.Domain.Models;
using Invoice_Gen.ViewModels;

namespace Invoice_Gen.Mappers;

public class ClientNameViewModelMapper : IMapper<ClientViewModel, Client>
{
    public Client Convert(ClientViewModel source)
    {
        throw new NotImplementedException();
    }

    public ClientViewModel Convert(Client destination) =>
        new()
        {
            ClientId = destination.ClientId,
            ClientName = destination.ClientName,
            ClientAddress = destination.ClientAddress,
            ContactName = destination.ContactName,
            ContactEmail = destination.ContactEmail,
        };
}
