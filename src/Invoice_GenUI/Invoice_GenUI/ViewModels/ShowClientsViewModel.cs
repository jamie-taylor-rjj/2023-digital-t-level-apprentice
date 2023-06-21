using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ShowClientsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;
        private readonly IClientService _clientService;
        public ObservableCollection<CreateClientModel> ShowClientDetails { get; } = new ObservableCollection<CreateClientModel>();

        public ShowClientsViewModel(INavigationService navService, IClientService clientService)
        {
            _navigation = navService;
            _clientService = clientService;
            GetClientDetails();
        }
        public int details { get; set; }
       
        public async Task GetClientDetails()
        {
            var tempClients = await _clientService.GetClientDetails();

            if (tempClients.Count != 0)
            {
                foreach (var clientName in tempClients)
                {
                    ShowClientDetails.Add(clientName);
                }
            }
        }
        [RelayCommand]
        private void GoBack()
        {
            _navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private void ClientDetails(object clientDetails)
        {
            _navigation.NavigateTo<ClientDetailsViewModel>();
        }
    }
}
