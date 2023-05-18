using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Invoice_GenUI.ViewModels
{
    public partial class InvoiceViewModel : ObservableObject
    {
        [ObservableProperty] // Populating property
        public LineItemViewModel lineItem = new();

        public ObservableCollection<LineItemViewModel> LineItems { get; } = new ObservableCollection<LineItemViewModel>(); // Two-way observable list

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
