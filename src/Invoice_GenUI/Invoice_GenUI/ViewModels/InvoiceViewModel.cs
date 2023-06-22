using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;
        [ObservableProperty]
        private bool clientNameLoading;
        [ObservableProperty]
        public ClientNameModel _selectedClientName = new ClientNameModel();

        private readonly IClientService _clientService;
        public ObservableCollection<ClientNameModel> ClientNames { get; } = new ObservableCollection<ClientNameModel>();

        public InvoiceViewModel(INavigationService navService, IClientService clientService)
        {
            _navigation = navService;
            _clientService = clientService;
        }
        [RelayCommand]
        private void GoBack()
        {
            _navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private void GoToLineItem()
        {
            _navigation.NavigateTo<AddLineItemViewModel>();
        }
        [RelayCommand]
        private async Task GetClientNames()
        {
            ClientNameLoading = true;

            var tempClients = await _clientService.GetClientNames();

            if (tempClients.Count != 0)
            {
                ClientNames.Clear();

                foreach (var clientName in tempClients)
                {
                    ClientNames.Add(clientName);
                }
            }
            ClientNameLoading = false;
        }
    }
}
