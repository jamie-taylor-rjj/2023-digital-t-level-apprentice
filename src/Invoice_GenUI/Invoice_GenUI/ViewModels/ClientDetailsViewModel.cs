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

        public List<string> getClient { get; } = new List<string> { "ABUH", "BUBA", "LALA", "AAAAA" };

        public ClientDetailsViewModel(INavigationService navService, IClientService clientService, ShowClientsViewModel showClientsViewModel)
        {
            _navigation = navService;
            _clientService = clientService;
            _showClientsViewModel = showClientsViewModel;
            GetClientID();
        }
        public string Name
        {
            get => getClient[0];
            set
            {
                Name = value;
            }
        }
        public string Contact
        {
            get => getClient[1];
            set
            {
                Contact = value;
            }
        }
        public string Email
        {
            get => getClient[2];
            set
            {
                Email = value;
            }
        }
        public string Address
        {
            get => getClient[3];
            set
            {
                Address = value;
            }
        }
        private async Task GetClientID()
        {
            var tempClients = await _clientService.GetSingleClientDetails(_showClientsViewModel.details.ToString());

            if (tempClients.Count != 0)
            {
                getClient.Clear();

                foreach (var clientName in tempClients)
                {
                    getClient.Add(clientName);
                }
            }
        }

        [RelayCommand]
        private void GoBack()
        {
            MessageBox.Show(_showClientsViewModel.details.ToString());
            _navigation.NavigateTo<ShowClientsViewModel>();
        }
    }
}
