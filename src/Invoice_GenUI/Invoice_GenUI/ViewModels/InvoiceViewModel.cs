using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceViewModel : ViewModel
    {
        // Invoice creation properties
        [ObservableProperty]
        private DateTime _issueDate;
        [ObservableProperty]
        private DateTime _dueDate;
        [ObservableProperty, NotifyDataErrorInfo]
        [Required]
        [Range(0.01, 100)]
        private double _vatRate;
        [ObservableProperty]
        private double _total;

      

        [ObservableProperty]
        private INavigationService _navigation;
        [ObservableProperty]
        private bool _clientNameLoading;
        [ObservableProperty]
        private ClientNameModel _selectedClientName = new ClientNameModel();
        
        private AddLineItemViewModel _addLineItemViewModel;

        private readonly IClientService _clientService;
        public ObservableCollection<LineItemModel> LineItems { get; } = new ObservableCollection<LineItemModel>();
        public ObservableCollection<ClientNameModel> ClientNames { get; } = new ObservableCollection<ClientNameModel>();

        public InvoiceViewModel(INavigationService navService, IClientService clientService, AddLineItemViewModel addLineItemViewModel)
        {
            PopulateGrid();
            _total = CalculateTotal();
            _dueDate = DateTime.Now.AddDays(1);
            _issueDate = DateTime.Now;
            _navigation = navService;
            _clientService = clientService;
            _addLineItemViewModel = addLineItemViewModel;
        }
        public void PopulateGrid()
        {
            if (LineItems.Count != 0)
            {
                LineItems.Clear();
            }
            foreach (var item in _addLineItemViewModel.newLineItems)
            {
                LineItems.Add(item);
            }
        }
        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in LineItems)
            {
                total = +item.Total;
            }
            return total;
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
        private void CreateInvoice()
        {
           MessageBoxResult result = MessageBox.Show("Do you want to create this invoice?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //DateTime test = Convert.ToDateTime(IssueDate.ToString("yyyy/MM/dd"));
            //MessageBox.Show(test.ToString("yyyy/MM/dd"));
        
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
            }
        }
    }
}
