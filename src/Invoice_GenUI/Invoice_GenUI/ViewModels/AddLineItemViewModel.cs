using System.ComponentModel.DataAnnotations;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class AddLineItemViewModel : ViewModel
    {
        private LineItemModel newItem;
        [ObservableProperty]
        private INavigationService _navigation;

        public AddLineItemViewModel(INavigationService navService)
        {
            Navigation = navService;
            newItem = new LineItemModel();
        }

        public double TotalResult()
        {
            if(Cost == null || Quantity == null)
            {
                return 0;
            }
            else
            {
                return Cost *Quantity;
            }
        }

        [Required(ErrorMessage = "Field is required")]
        public string? Description
        {
            get => newItem.Description;
            set
            {
                newItem.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        [Required(ErrorMessage = "Field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int Quantity
        {
            get => newItem.Quantity;
            set
            {
                newItem.Quantity = value;
                OnPropertyChanged(nameof(Quantity));

                newItem.Total = TotalResult();
                OnPropertyChanged(nameof(Total));
            }
        }
        [Required(ErrorMessage = "Field is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public double Cost
        {
            get => newItem.Cost;
            set
            {
                newItem.Cost = value;
                OnPropertyChanged(nameof(Cost));

                newItem.Total = TotalResult();
                OnPropertyChanged(nameof(Total));
            }
        }
        public double Total
        {
            get => newItem.Total;
            set
            {
                newItem.Total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
        [RelayCommand]
        public void CancelLineItem()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                Description = string.Empty;
                Quantity = 0;
                Cost = 0;
                Total = 0;

                Navigation.NavigateTo<InvoiceViewModel>();
            }
        }
    }
}
