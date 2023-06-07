using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;

namespace Invoice_GenUI.ViewModels;
public class ClientPostModel
{
    public string ClientName { get; set; }
    public string ClientAddress { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }

}
public partial class CreateClientViewModel : ViewModelBase
{
    private readonly IClientService _clientService;
    private CreateClientModel newClient;
    private string client;


    public CreateClientViewModel(IClientService clientService)
    {
        newClient = new CreateClientModel();
        _clientService = clientService;
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
    [RelayCommand] // async response
    public async Task CreateClient()
    {

        if (string.IsNullOrWhiteSpace(clientName) ||
            string.IsNullOrWhiteSpace(clientAddress) ||
            string.IsNullOrWhiteSpace(contactName) ||
            string.IsNullOrWhiteSpace(contactEmail))
        {
            MessageBox.Show("Invalid data");

        }
        else
        {
            var connected = await _clientService.PutClient(clientName, clientAddress, contactName, contactEmail); // make it return bool value
            bool result = connected; // The bool value result
            MessageBox.Show($"{result}");
            if (result)
            {
                clientName = string.Empty;
                contactName = string.Empty;
                clientAddress = string.Empty;
                contactEmail = string.Empty;
            }
        }
    }
}
