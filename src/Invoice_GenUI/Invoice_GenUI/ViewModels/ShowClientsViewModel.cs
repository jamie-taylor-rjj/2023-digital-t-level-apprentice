using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.InternalServices;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ShowClientsViewModel : ViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IClientService _clientService;
        private readonly IPassingService _passingService;
        private readonly IMessageBoxService _messageBoxService;
        public ShowClientsViewModel(INavigationService navService, IClientService clientService, IPassingService passingService, IMessageBoxService messageBoxService)
        {
            _navigation = navService;
            _clientService = clientService;
            _passingService = passingService;
            _messageBoxService = messageBoxService;
            Task.Run(() => LoadClients()).Wait();

        }
        [ObservableProperty]
        private int _currentPage=1;
        [ObservableProperty]
        private int _numberOfPages;
        [ObservableProperty]
        private int _clientAmnt=10;
        [ObservableProperty]
        private ObservableCollection<CreateClientModel>? _showClientDetails;
        public async Task LoadClients()
        {
            int pageNumber = CurrentPage;
            int pageSize = ClientAmnt;
            var pagedClients = await _clientService.GetClientPages(pageNumber, pageSize);
            ShowClientDetails = pagedClients.Data;
            NumberOfPages = pagedClients.TotalPages;
        }
        [RelayCommand]
        private async void NextPage()
        {
            if(CurrentPage < NumberOfPages)
            {
                CurrentPage++;
                await LoadClients();
            }
        }
        [RelayCommand]
        private async void PrevPage()
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                await LoadClients();
            }
        }
        [RelayCommand]
        private void GoBack()
        {
            _navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private void ClientDetails(CreateClientModel parameter)
        {
            _passingService.ClientID = parameter.ClientId;
            _navigation.NavigateTo<ClientDetailsViewModel>();
        }
        [RelayCommand]
        private async void DeleteClientDetails(CreateClientModel parameter)
        {
            var result = _messageBoxService.Confirm("Are you sure you want to delete this client?");
            if (result)
            {
                bool confirm = await _clientService.DeleteClient(parameter.ClientId);
                if (confirm)
                {
                    _messageBoxService.Success("Client has been deleted");
                    _navigation.NavigateTo<ShowClientsViewModel>();
                }
                else
                {
                    _messageBoxService.Failed("Failed to delete client");
                }
            }
        }
    }
}
