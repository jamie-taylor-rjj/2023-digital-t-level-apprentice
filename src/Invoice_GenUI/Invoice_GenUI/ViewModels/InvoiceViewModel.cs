using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.InternalServices;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceViewModel : ViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IPassingService _passingService;
        private readonly IInvoiceService _invoiceService;
        private readonly IClientService _clientService;
        private readonly IMessageBoxService _messageBoxService;

        public ObservableCollection<LineItemModel> LineItems => _passingService.StoredItems!;
        public ObservableCollection<ClientNameModel> ClientNames { get; } = new ObservableCollection<ClientNameModel>();

        public InvoiceViewModel(INavigationService navService, IClientService clientService, IInvoiceService invoiceService, IPassingService passingService, IMessageBoxService messageBoxService)
        {
            _dueDate = DateTime.Now.AddDays(1);
            _issueDate = DateTime.Now;
            _navigation = navService;
            _clientService = clientService;
            _invoiceService = invoiceService;
            _passingService = passingService;
            _messageBoxService = messageBoxService;
            _total = CalculateTotal();
        }
        [ObservableProperty]
        private bool _clientNameLoading;
        [ObservableProperty]
        private ClientNameModel _selectedClientName = new ClientNameModel();
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
            var result = _messageBoxService.Warning("All your progress will be cleared");
            if (result == true)
            {
                _passingService.StoredItems!.Clear();
                _navigation.NavigateTo<HomeViewModel>();
            }
        }
        [RelayCommand]
        private void GoToLineItem()
        {
            _navigation.NavigateTo<AddLineItemViewModel>();
        }
        [RelayCommand]
        private async Task GetClientNames()
        {
            ClientNameLoading = true;

            var tempClients = await _clientService.GetClientNames();
            foreach (var clientName in tempClients)
            {
                ClientNames.Add(clientName);
            }

            ClientNameLoading = false;
        }
        [RelayCommand]
        private async void CreateInvoice()
        {
            var result = _messageBoxService.Confirm("Are you sure you want to create this invoice?");
            if (result == true)
            {
                if (SelectedClientName.ClientID == 0 || SelectedClientName.ClientName == null)
                {
                    _messageBoxService.ValidationError("A client has not been selected");
                }
                else if (LineItems.Count == 0)
                {
                    _messageBoxService.ValidationError("Must have at least one item in the grid");
                }
                else if (IssueDate > DueDate)
                {
                    _messageBoxService.ValidationError("The issue date must be before the due date\nThe due date must be after the issue date");
                }
                else if (Total < 0)
                {
                    _messageBoxService.ValidationError("The total must be over 0");
                }
                else if (VatRate > 25 || VatRate < 0)
                {
                    _messageBoxService.ValidationError("The VAT rate must be a positive integer no higher than 25");
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
                        _messageBoxService.Success("Invoice successfully created");
                    }
                    else
                    {
                        _messageBoxService.Failed("Failed to create invoice");
                    }
                }
            }
        }
    }
}
