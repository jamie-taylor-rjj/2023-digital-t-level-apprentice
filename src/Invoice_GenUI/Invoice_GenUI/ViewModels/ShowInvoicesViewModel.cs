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
        private double _total;

        [ObservableProperty]
        private INavigationService _navigation;
        private readonly IInvoiceListService _invoiceListService;

        public ObservableCollection<InvoiceModel> DisplayInvoices { get; } = new ObservableCollection<InvoiceModel>();

        public ShowInvoicesViewModel(INavigationService navService, IInvoiceListService invoiceListService)
        {
            _navigation = navService;
            _invoiceListService = invoiceListService;
            Task.Run(() => GetInvoiceDetails()).Wait();
            _total = CalculateTotal();
        }
        public double CalculateTotal()
        {
            double total = 0;
            foreach(var item in DisplayInvoices)
            {
              foreach(var line in item.LineItems)
                {
                    total += line.Cost;
                }
            }

            return total;
        }
        public async Task GetInvoiceDetails()
        {
            var tempInvoices = await _invoiceListService.GetInvoices();

            if (tempInvoices.Count != 0)
            {
                foreach (var invoice in tempInvoices)
                {
                    DisplayInvoices.Add(invoice);
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
