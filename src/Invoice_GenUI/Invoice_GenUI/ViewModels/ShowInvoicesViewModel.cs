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
        public int InvoiceID { get; set; }
        public ShowInvoicesViewModel(INavigationService navService, IInvoiceListService invoiceListService)
        {
            _navigation = navService;
            _invoiceListService = invoiceListService;
            Task.Run(() => GetInvoiceDetails()).Wait();
            AssignIDs();
            AssignTotal();
        }
        public void AssignIDs()
        {
            int idCounter = 1;
            foreach(var item in DisplayInvoices)
            {
                item.InvoiceId = idCounter;
                idCounter++;
            }
        }
        public void AssignTotal()
        {
            double total = 0;
            double vatTotal = 0;
            foreach(var item in DisplayInvoices)
            {
                foreach(var lineItem in item.LineItems)
                {
                    total = lineItem.Cost * lineItem.Quantity;
                    vatTotal = total * (item.VatRate / 100);
                    item.Total = total + vatTotal;

                    total = 0;
                    vatTotal = 0;
                }
            }
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
        [RelayCommand]
        public void ViewInvoiceDetails(object parameter)
        {
            if(parameter is InvoiceModel details)
            {
                InvoiceID = details.ClientId;
            }
            Navigation.NavigateTo<InvoiceDetailsViewModel>();
        }
    }
}
