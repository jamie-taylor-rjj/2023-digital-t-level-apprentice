using Invoice_GenUI.Models;

namespace Invoice_GenUI.ViewModels;
public class CreateClientViewModel : ViewModelBase
{
    private CreateClientModel newClient;
    private string client;


    public CreateClientViewModel()
    {
        newClient = new CreateClientModel();
    }
    public string clientName
    {
        get => newClient.ClientName;
        set
        {
            newClient.ClientName = value;
            OnPropertyChanged(nameof(clientName));
        }
    }

    public string clientAddress
    {
        get => newClient.ClientAddress;
        set
        {
            newClient.ClientAddress = value;
            OnPropertyChanged(nameof(clientAddress));
        }
    }
    public string contactName
    {
        get => newClient.ContactName;
        set
        {
            newClient.ContactName = value;
            OnPropertyChanged(nameof(contactName));
        }
    }
    public string contactEmail
    {
        get => newClient.ContactEmail;
        set
        {
            newClient.ContactEmail = value;
            OnPropertyChanged(nameof(contactEmail));
        }
    }
}
