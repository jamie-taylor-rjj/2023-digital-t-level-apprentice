using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
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
        private async void DeleteClientDetails(CreateClientModel parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this client?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bool confirm = await _clientService.DeleteClient(parameter.ClientId);
                if (confirm)
                {
                    MessageBox.Show("Client has been deleted", "DELETED", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Navigation.NavigateTo<ShowClientsViewModel>();
                }
                else
                {
                    MessageBox.Show("Failed to delete client", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
