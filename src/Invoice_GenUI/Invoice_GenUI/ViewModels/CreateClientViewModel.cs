using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.InternalServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class CreateClientViewModel : ViewModel
    {
        private readonly IClientService _clientService;
        private readonly INavigationService _navigation;
        private readonly IMessageBoxService _messageBoxService;

        public CreateClientViewModel(INavigationService navService, IClientService clientService, IMessageBoxService messageBoxService)
        {
            _navigation = navService;
            _clientService = clientService;
            _messageBoxService = messageBoxService;
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
            _navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private async Task CreateClient()
        {
            var confirm = _messageBoxService.Confirm("Do you want to create this client?");
            if (confirm)
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
                    _messageBoxService.Success("Client has been created");

                    ClientName = string.Empty;
                    ContactEmail = string.Empty;
                    ClientAddress = string.Empty;
                    ContactName = string.Empty;
                }
                else
                {
                    _messageBoxService.Failed("Failed to create client");
                }
            }
        }
    }
}
