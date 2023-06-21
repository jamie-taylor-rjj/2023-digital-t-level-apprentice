using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ClientDetailsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;
        private IClientService _clientService;
        private ShowClientsViewModel _showClientsViewModel;

        public ObservableCollection<SingleClientModel> getClient { get; } = new ObservableCollection<SingleClientModel>();

        public ClientDetailsViewModel(INavigationService navService, IClientService clientService, ShowClientsViewModel showClientsViewModel)
        {
            _navigation = navService;
            _clientService = clientService;
            _showClientsViewModel = showClientsViewModel;
            GetClientID();
        }
        public int index => _showClientsViewModel.details + 1;
        public string Name => "banana";
        public string Contact => "apple";
        public string Email => "grape";
        public string Address => "pineapple";
        
        private async Task GetClientID()
        {
            var tempClients = await _clientService.GetSingleClientDetails(index);

            if (tempClients.Count != 0)
            {
                foreach (var clientName in tempClients)
                {
                    getClient.Add(clientName);
                }
            }
        }

        [RelayCommand]
        private void GoBack()
        {
            _navigation.NavigateTo<ShowClientsViewModel>();
        }
    }
}
