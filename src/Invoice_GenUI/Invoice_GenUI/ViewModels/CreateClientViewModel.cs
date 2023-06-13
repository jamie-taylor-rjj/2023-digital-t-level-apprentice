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
    public string ClientName
    {
        get => newClient.ClientName;
        set
        {
            newClient.ClientName = value;
            OnPropertyChanged(nameof(ClientName));
        }
    }

    public string ClientAddress
    {
        get => newClient.ClientAddress;
        set
        {
            newClient.ClientAddress = value;
            OnPropertyChanged(nameof(ClientAddress));
        }
    }
    public string ContactName
    {
        get => newClient.ContactName;
        set
        {
            newClient.ContactName = value;
            OnPropertyChanged(nameof(ContactName));
        }
    }
    public string ContactEmail
    {
        get => newClient.ContactEmail;
        set
        {
            newClient.ContactEmail = value;
            OnPropertyChanged(nameof(ContactEmail));
        }
    }
}
