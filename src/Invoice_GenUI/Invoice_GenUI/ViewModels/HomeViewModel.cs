using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class HomeViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;

        public HomeViewModel(INavigationService navService)
        {
            _navigation = navService;
        }
        [RelayCommand]
        private void GoToInvoice()
        {
            Navigation.NavigateTo<InvoiceViewModel>();
        }
        [RelayCommand]
        private void GoToCreateClient()
        {
            Navigation.NavigateTo<CreateClientViewModel>();
        }
        [RelayCommand]
        private void GoToShowClients()
        {
            Navigation.NavigateTo<ShowClientsViewModel>();
        }
        [RelayCommand]
        private void GoToInvoices() 
        {
            Navigation.NavigateTo<ShowInvoicesViewModel>();
        }
    }
}
