using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceDetailsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;

        public InvoiceDetailsViewModel(INavigationService navService)
        {
            _navigation = navService;
        }
       
        [RelayCommand]
        public void GoBack()
        {
            Navigation.NavigateTo<ShowInvoicesViewModel>();
        }
    }
}
