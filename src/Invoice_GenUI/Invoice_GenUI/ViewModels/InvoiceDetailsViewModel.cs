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
        private readonly IInvoiceService _invoiceService;
        private readonly ShowInvoicesViewModel _showInvoicesViewModel;

        public InvoiceDetailsViewModel(INavigationService navService, IInvoiceService invoiceService, ShowInvoicesViewModel showInvoicesViewModel)
        {
            _navigation = navService;
            _invoiceService = invoiceService;
            _showInvoicesViewModel = showInvoicesViewModel;
        }



        [RelayCommand]
        public void GoBack()
        {
            Navigation.NavigateTo<ShowInvoicesViewModel>();
        }
    }
}
