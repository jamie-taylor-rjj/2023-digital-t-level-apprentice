using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.PassingValuesServices;
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
        private readonly IPassingService _passingService;

        public ObservableCollection<InvoiceModel> DisplayInvoices { get; } = new ObservableCollection<InvoiceModel>();
        public int DisplayedID { get; set; }
        public ShowInvoicesViewModel(INavigationService navService, IInvoiceListService invoiceListService, IPassingService passingService)
        {
            _passingService = passingService;
            _navigation = navService;
            _invoiceListService = invoiceListService;
            Task.Run(() => GetInvoiceDetails()).Wait();
            AssignIDs();
            AssignTotal();
        }
        public void AssignIDs()
        {
            int idCounter = 1;
            foreach (var item in DisplayInvoices)
            {
                DisplayedID = idCounter;
                idCounter++;
            }
            idCounter = 0;
        }
        public void AssignTotal()
        {
            double total = 0;
            foreach (var item in DisplayInvoices)
            {
                foreach (var lineItem in item.LineItems)
                {
                    total += lineItem.Cost * lineItem.Quantity;
                    double vatTotal = total * (item.VatRate / 100);
                    item.Total = total + vatTotal;
                }
                total = 0;
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
        public void ViewInvoiceDetails(InvoiceModel parameter)
        {
            _passingService.InvoiceID = parameter.InvoiceId;
            Navigation.NavigateTo<InvoiceDetailsViewModel>();
        }
        [RelayCommand]
        public async void DeleteInvoiceDetails(InvoiceModel parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this invoice?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bool confirm = await _invoiceListService.DeleteInvoice(parameter.InvoiceId);
                if (confirm)
                {
                    MessageBox.Show("Invoice has been deleted", "DELETED", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Navigation.NavigateTo<ShowInvoicesViewModel>();
                }
                else
                {
                    MessageBox.Show("Failed to delete invoice", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
