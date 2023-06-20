using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class CreateClientViewModel : ViewModel
    {
        private CreateClientModel newClient;
        private readonly IClientService _clientService;

        [ObservableProperty]
        private INavigationService _navigation;

        public CreateClientViewModel(INavigationService navService, IClientService clientService)
        {
            Navigation = navService;
            _clientService = clientService;
            newClient = new CreateClientModel();
        }

        [Required(ErrorMessage = "Field is required")]
        public string? ClientName
        {
            get => newClient.ClientName;
            set
            {
                newClient.ClientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }
        [Required(ErrorMessage = "Field is required")]
        public string? ContactName
        {
            get => newClient.ContactName;
            set
            {
                newClient.ContactName = value;
                OnPropertyChanged(nameof(ContactName));
            }
        }
        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? ContactEmail
        {
            get => newClient.ContactEmail;
            set
            {
                newClient.ContactEmail = value;
                OnPropertyChanged(nameof(ContactEmail));
            }
        }
        [Required(ErrorMessage = "Field is required")]
        public string? ClientAddress
        {
            get => newClient.ClientAddress;
            set
            {
                newClient.ClientAddress = value;
                OnPropertyChanged(nameof(ClientAddress));
            }
        }
        
        
        [RelayCommand]
        public void GoBack()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        public async Task CreateClient()
        {
            var connected = await _clientService.PutClient(newClient); // make it return bool value
            bool result = connected; // The bool value result
            MessageBox.Show("Client has been created","Success", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result)
            {
                ClientName = string.Empty;
                ContactEmail = string.Empty;
                ClientAddress = string.Empty;
                ContactName = string.Empty;
            }
            else
            {
                MessageBox.Show("Failed to create client","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
