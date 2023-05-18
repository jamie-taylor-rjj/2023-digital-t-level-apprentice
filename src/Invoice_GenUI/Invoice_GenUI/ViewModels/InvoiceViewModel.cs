using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceViewModel : ObservableObject
    {
        [ObservableProperty] // Populating property
        public LineItemViewModel lineItem = new();

        public ObservableCollection<LineItemViewModel> LineItems { get; } = new ObservableCollection<LineItemViewModel>(); // Two-way observable list

        public ObservableCollection<ClientNameViewModel> ClientNames { get; } = new ObservableCollection<ClientNameViewModel>();

        public InvoiceViewModel()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://2023-invoice-gen.azurewebsites.net/");

                var response = client.GetAsync("clients").Result; // http request for base address + clients

                response.EnsureSuccessStatusCode(); // Makes sure response is valid

                var temp = response.Content.ReadFromJsonAsync<List<ClientNameViewModel>>().Result;

                if (temp != null)
                {
                    foreach (var item in temp)
                    {
                        ClientNames.Add(item);
                    }
                }

            }
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
    }
}
