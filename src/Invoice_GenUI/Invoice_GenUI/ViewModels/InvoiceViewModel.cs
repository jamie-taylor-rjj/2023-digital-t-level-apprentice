using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceViewModel : ViewModel
    {
        [ObservableProperty]
        private DateTime _issueDate;
        [ObservableProperty]
        private DateTime _dueDate;
        [ObservableProperty]
        private double _total;
        [ObservableProperty]
        private double _invoiceTotal;
        private double _vatRate;
        [Required]
        [Range(1, 25)]
        public double VatRate
        {
            get => _vatRate;
            set
            {
                _vatRate = value;
                OnPropertyChanged(nameof(VatRate));

                if (_vatRate > 0.01 && _vatRate <= 25)
                {
                    InvoiceTotal = CalculateInvoiceTotal();
                    OnPropertyChanged(nameof(InvoiceTotal));
                }
            }
        }

        [ObservableProperty]
        private INavigationService _navigation;
        [ObservableProperty]
        private bool _clientNameLoading;
        [ObservableProperty]
        private ClientNameModel _selectedClientName = new ClientNameModel();

        private readonly IPassingService _passingService;
        private readonly IInvoiceService _invoiceService;
        private readonly IClientService _clientService;
        public ObservableCollection<LineItemModel> LineItems  => _passingService.StoredItems!;
        public ObservableCollection<ClientNameModel> ClientNames { get; } = new ObservableCollection<ClientNameModel>();

        public InvoiceViewModel(INavigationService navService, IClientService clientService, IInvoiceService invoiceService, IPassingService passingService)
        {
            _dueDate = DateTime.Now.AddDays(1);
            _issueDate = DateTime.Now;
            _navigation = navService;
            _clientService = clientService;
            _invoiceService = invoiceService;
            _passingService = passingService;
            _total = CalculateTotal();
        }
        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in LineItems)
            {
                total += item.Total;
            }
            return total;
        }
        private double CalculateInvoiceTotal()
        {
            double rate = Total * VatRate / 100;
            return rate + Total;
        }
        [RelayCommand]
        private void GoBack()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private void GoToLineItem()
        {
            Navigation.NavigateTo<AddLineItemViewModel>();
        }
        [RelayCommand]
        private async Task GetClientNames()
        {
            ClientNameLoading = true;

            var tempClients = await _clientService.GetClientNames();

            if (tempClients.Count != 0)
            {
                ClientNames.Clear();

                foreach (var clientName in tempClients)
                {
                    ClientNames.Add(clientName);
                }
            }

            ClientNameLoading = false;
        }
        [RelayCommand]
        private async void CreateInvoice()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to create this invoice?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (SelectedClientName.ClientID == 0 || SelectedClientName.ClientName == null)
                {
                    MessageBox.Show("A client has not been selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (LineItems.Count == 0)
                {
                    MessageBox.Show("Must have at least one item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (IssueDate > DueDate)
                {
                    MessageBox.Show("The issue date must be before the due date\nThe due date must be after the issue date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Total < 0)
                {
                    MessageBox.Show("The total must be over 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (VatRate > 25 || VatRate < 0)
                {
                    MessageBox.Show("The VAT rate must be a positive integer no higher than 25", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var newInvoice = new InvoiceModel
                    {
                        ClientId = SelectedClientName.ClientID,
                        IssueDate = IssueDate,
                        DueDate = DueDate,
                        VatRate = VatRate,
                        LineItems = LineItems
                    };
                    var connected = await _invoiceService.PutInvoice(newInvoice);
                    bool isConnect = connected;
                    if (isConnect)
                    {
                        MessageBox.Show("Invoice has been created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Invoice creation failed", "Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }
    }
}
