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
        private readonly IClientService _clientService;

        [ObservableProperty]
        private INavigationService _navigation;

        public CreateClientViewModel(INavigationService navService, IClientService clientService)
        {
            _navigation = navService;
            _clientService = clientService;
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Field is required")]
        private string? _clientName;
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Field is required")]
        private string? _contactName;
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        private string? _contactEmail;
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Field is required")]
        private string? _clientAddress;

        [RelayCommand]
        private void GoBack()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private async Task CreateClient()
        {
            MessageBoxResult confirm = MessageBox.Show("Do you want to create this client?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                var newClient = new CreateClientModel
                {
                    ClientName = ClientName,
                    ContactEmail = ContactEmail,
                    ClientAddress = ClientAddress,
                    ContactName = ContactName
                };

                var connected = await _clientService.PutClient(newClient);
                bool result = connected;
                if (result)
                {
                    MessageBox.Show("Client has been created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    ClientName = string.Empty;
                    ContactEmail = string.Empty;
                    ClientAddress = string.Empty;
                    ContactName = string.Empty;
                }
                else
                {
                    MessageBox.Show("Failed to create client", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
