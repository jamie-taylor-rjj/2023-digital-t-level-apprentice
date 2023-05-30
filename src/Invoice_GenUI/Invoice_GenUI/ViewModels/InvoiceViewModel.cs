using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceViewModel : ObservableObject // Observable object is closed for modification but opened it up for extension
    {
        private readonly IClientService _clientService;

        [ObservableProperty]
        private bool clientNameLoading;

        [ObservableProperty] // Populating property
        public LineItemViewModel lineItem = new();

        public ObservableCollection<LineItemViewModel> LineItems { get; } = new ObservableCollection<LineItemViewModel>(); // Two-way observable list

        public ObservableCollection<ClientNameViewModel> ClientNames { get; } = new ObservableCollection<ClientNameViewModel>();

        [ObservableProperty]
        public ClientNameViewModel _selectedClientName = new ClientNameViewModel();

        public InvoiceViewModel(IClientService clientService)
        {
            _clientService = clientService;
            
            
            LineItems.Add(new LineItemViewModel
            {
                Description = Guid.NewGuid().ToString(),
                Quantity = 2,
                Cost = 9.99
            });
        }

        [RelayCommand]
        public async Task GetClientNames()
        {
            ClientNameLoading = true; // when button clicked 

            var tempClients = await _clientService.GetClientNames();

            if (tempClients.Count != 0) 
            {
                ClientNames.Clear();

                foreach (var clientName in tempClients)
                {
                    ClientNames.Add(clientName);
                }
            }
           ClientNameLoading = false; // when all names loaded in
        }

        [RelayCommand]
        public void AddLineItem() 
        {
            var newLineItem = new LineItemViewModel // Object initialisation
            {
                Description = lineItem.Description,
                Quantity = lineItem.Quantity,
                Cost = lineItem.Cost,
            };
            LineItems.Add(newLineItem);
        }
        public double LineItemsTotal()
        {
            var runningTotal = 0.0;

            foreach (var item in LineItems)
            {
                runningTotal += item.Total();
            }
            return runningTotal;
        }
    }
}
