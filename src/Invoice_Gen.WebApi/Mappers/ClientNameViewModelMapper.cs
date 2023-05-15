using Invoice_Gen.ViewModels;

namespace Invoice_Gen.WebApi.Mappers;

public class ClientNameViewModelMapper : IMapper<ClientNameViewModel, Client>
{
    public Client Convert(ClientNameViewModel source)
    {
        throw new NotImplementedException();
    }

    public ClientNameViewModel Convert(Client destination) =>
        new() { ClientName = destination.ClientName, ClientId = destination.ClientId };
}
