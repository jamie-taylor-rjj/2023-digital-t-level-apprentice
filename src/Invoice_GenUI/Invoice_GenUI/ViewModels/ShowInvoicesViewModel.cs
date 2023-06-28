using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ShowInvoicesViewModel : ViewModel
    {
        [ObservableProperty]
        private readonly INavigationService _navigation;
        private readonly IInvoiceListService _invoiceListService;

        public ObservableCollection<InvoiceModel> displayInvoices = new ObservableCollection<InvoiceModel>();

        public ShowInvoicesViewModel(INavigationService navService, IInvoiceListService invoiceListService)
        {
            _navigation = navService;
            _invoiceListService = invoiceListService;
            Task.Run(() => GetInvoiceDetails()).Wait();
        }
        public async Task GetInvoiceDetails()
        {
            var tempInvoices = await _invoiceListService.GetInvoices(1);

            if (tempInvoices.Count != 0)
            {
                foreach (var clientName in tempInvoices)
                {
                    displayInvoices.Add(clientName);
                }
            }
        }
        [RelayCommand]
        public void GoBack()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
