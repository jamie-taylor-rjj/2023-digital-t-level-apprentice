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
            Navigation = navService;
        }
        [RelayCommand]
        public void GoToInvoice()
        {
            Navigation.NavigateTo<InvoiceViewModel>();
        }
    }
}
