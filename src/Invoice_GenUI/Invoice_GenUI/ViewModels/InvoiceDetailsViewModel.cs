﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceDetailsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;
        private readonly IInvoiceService _invoiceService;
        private readonly IClientService _clientService;
        private readonly IPassingService _passingService;
       
        public ObservableCollection<LineItemModel> LineItemDetails { get; set; }

        public InvoiceDetailsViewModel(INavigationService navService, IInvoiceService invoiceService, IClientService clientService, IPassingService passingService)
        {
            _passingService = passingService;
            _navigation = navService;
            _invoiceService = invoiceService;
            _clientService = clientService;
            Task.Run(() => GetInvoiceID()).Wait();
            AssignTotal();
        }
        public string? ClientName { get; set; }
        public double VatRate { get; set; }
        public double Total { get; set; }
        public double InvoiceTotal { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        private async Task GetInvoiceID()
        {
            var singleInvoice = await _invoiceService.GetSingleInvoiceDetails(_passingService.InvoiceID);

            VatRate = singleInvoice.VatRate;
            IssueDate = singleInvoice.IssueDate.Date;
            DueDate = singleInvoice.DueDate.Date;
            LineItemDetails = singleInvoice.LineItems;

            foreach (var item in singleInvoice.LineItems)
            {
                Total += item.Cost * item.Quantity;
                var vatTotal = Total * (singleInvoice.VatRate / 100);
                InvoiceTotal = Total + vatTotal;
            }

            int id = singleInvoice.ClientId;
            var singleClient = await _clientService.GetSingleClientDetails(id);

            ClientName = singleClient.ClientName ?? string.Empty;
        }
        public void AssignTotal()
        {
            foreach (var lineItems in LineItemDetails)
            {
                lineItems.Total = lineItems.Quantity * lineItems.Cost;
            }
        }

        [RelayCommand]
        public void GoBack()
        {
            Navigation.NavigateTo<ShowInvoicesViewModel>();
        }
    }
}