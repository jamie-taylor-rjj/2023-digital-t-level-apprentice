using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ShowClientsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;
        private readonly IClientService _clientService;
        private readonly IPassingService _passingService;
        public ObservableCollection<CreateClientModel> ShowClientDetails { get; } = new ObservableCollection<CreateClientModel>();

        public ShowClientsViewModel(INavigationService navService, IClientService clientService, IPassingService passingService)
        {
            _navigation = navService;
            _clientService = clientService;
            _passingService = passingService;
            Task.Run(() => GetClientDetails()).Wait();
        }
        public async Task GetClientDetails()
        {
            var tempClients = await _clientService.GetClientDetails();

            if (tempClients.Count != 0)
            {
                ShowClientDetails.Clear();
                foreach (var clientName in tempClients)
                {
                    ShowClientDetails.Add(clientName);
                }
            }
        }
        [RelayCommand]
        private void GoBack()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private void ClientDetails(CreateClientModel parameter)
        {
            _passingService.ClientID = parameter.ClientId;
            Navigation.NavigateTo<ClientDetailsViewModel>();
        }
        [RelayCommand]
        private void DeleteClientDetails(CreateClientModel parameter)
        {

        }
    }
}
