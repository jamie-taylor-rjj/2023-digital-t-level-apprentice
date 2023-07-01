using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class HomeViewModel : ViewModel
    {
        private readonly INavigationService _navigation;

        public HomeViewModel(INavigationService navService)
        {
            _navigation = navService;
        }
        [RelayCommand]
        private void GoToInvoice()
        {
            _navigation.NavigateTo<InvoiceViewModel>();
        }
        [RelayCommand]
        private void GoToCreateClient()
        {
            _navigation.NavigateTo<CreateClientViewModel>();
        }
        [RelayCommand]
        private void GoToShowClients()
        {
            _navigation.NavigateTo<ShowClientsViewModel>();
        }
        [RelayCommand]
        private void GoToInvoices()
        {
            _navigation.NavigateTo<ShowInvoicesViewModel>();
        }
    }
}
