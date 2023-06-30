using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ClientDetailsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;
        private readonly IClientService _clientService;
        private readonly IPassingService _passingService;

        public ClientDetailsViewModel(INavigationService navService, IClientService clientService, IPassingService passingService)
        {
            _navigation = navService;
            _clientService = clientService;
            _passingService = passingService;
            Task.Run(() => GetClientID()).Wait();
        }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        private async Task GetClientID()
        {
            var singleClient = await _clientService.GetSingleClientDetails(_passingService.ClientID);

            Name = singleClient.ClientName ?? string.Empty;
            Contact = singleClient.ContactName ?? string.Empty;
            Email = singleClient.ContactEmail ?? string.Empty;
            Address = singleClient.ClientAddress ?? string.Empty;
        }

        [RelayCommand]
        private void GoBack()
        {
            Navigation.NavigateTo<ShowClientsViewModel>();
        }
    }
}
