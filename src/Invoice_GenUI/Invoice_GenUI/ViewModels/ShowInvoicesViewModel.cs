using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.InternalServices;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ShowInvoicesViewModel : ViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IInvoiceListService _invoiceListService;
        private readonly IPassingService _passingService;
        private readonly IMessageBoxService _messageBoxService;

        public ObservableCollection<InvoiceModel> DisplayInvoices { get; } = new ObservableCollection<InvoiceModel>();
        public ShowInvoicesViewModel(INavigationService navService, IInvoiceListService invoiceListService, IPassingService passingService, IMessageBoxService messageBoxService)
        {
            _passingService = passingService;
            _navigation = navService;
            _invoiceListService = invoiceListService;
            _messageBoxService = messageBoxService;
            Task.Run(() => GetInvoiceDetails()).Wait();
            AssignTotal();
        }

        [ObservableProperty]
        private double _total;
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

            foreach (var invoice in tempInvoices)
            {
                DisplayInvoices.Add(invoice);
            }
        }
        [RelayCommand]
        public void GoBack()
        {
            _navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        public void ViewInvoiceDetails(InvoiceModel parameter)
        {
            _passingService.InvoiceId = parameter.InvoiceId;
            _navigation.NavigateTo<InvoiceDetailsViewModel>();
        }
        [RelayCommand]
        public async void DeleteInvoiceDetails(InvoiceModel parameter)
        {
            var result = _messageBoxService.Confirm("Are you sure you want to delete this invoice?");
            if (result)
            {
                bool confirm = await _invoiceListService.DeleteInvoice(parameter.InvoiceId);
                if (confirm)
                {
                    _messageBoxService.Success("Invoice successfully deleted");
                    _navigation.NavigateTo<ShowInvoicesViewModel>();
                }
                else
                {
                    _messageBoxService.Failed("Failed to delete invoice");
                }
            }
        }
    }
}
