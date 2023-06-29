using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        private readonly IClientService _clientService;
        private readonly ShowInvoicesViewModel _showInvoicesViewModel;

        public ObservableCollection<InvoiceModel> InvoiceDetails { get; } = new ObservableCollection<InvoiceModel>();

        public InvoiceDetailsViewModel(INavigationService navService, IInvoiceService invoiceService, ShowInvoicesViewModel showInvoicesViewModel, IClientService clientService)
        {
            _navigation = navService;
            _invoiceService = invoiceService;
            _showInvoicesViewModel = showInvoicesViewModel;
            _clientService = clientService;
            Task.Run(() => GetInvoiceID()).Wait();
        }

        public string? ClientName { get; set; }
        public double VatRate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        private async Task GetInvoiceID()
        {
            int ID;
            var singleInvoice = await _invoiceService.GetSingleInvoiceDetails(_showInvoicesViewModel.InvoiceID);
            
            VatRate = singleInvoice.VatRate;
            IssueDate = singleInvoice.IssueDate.Date;
            DueDate = singleInvoice.DueDate.Date;

            ID = singleInvoice.ClientId;
            var singleClient = await _clientService.GetSingleClientDetails(ID);

            ClientName = singleClient.ClientName ?? string.Empty;
        }

        [RelayCommand]
        public void GoBack()
        {
            Navigation.NavigateTo<ShowInvoicesViewModel>();
        }
    }
}
