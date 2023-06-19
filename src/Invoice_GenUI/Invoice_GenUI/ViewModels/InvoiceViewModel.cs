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

        public InvoiceViewModel(INavigationService navService)
        {
            Navigation = navService;
        }
        [RelayCommand]
        public void GoBack()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
