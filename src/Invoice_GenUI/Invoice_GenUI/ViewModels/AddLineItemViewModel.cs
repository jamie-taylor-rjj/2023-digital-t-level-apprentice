using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class AddLineItemViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;
        private readonly IPassingService _passingService;

        public ObservableCollection<LineItemModel> newLineItems { get; } = new ObservableCollection<LineItemModel>();

        public AddLineItemViewModel(INavigationService navService, IPassingService passingService)
        {
            _passingService = passingService;
            _navigation = navService;
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Field is required")]
        private string _description;
        private double _total;
        private double _cost;
        private int _quantity;
        public int ItemId;

        [Required(ErrorMessage = "Field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));

                Total = TotalResult();
            }
        }
        [Required(ErrorMessage = "Field is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public double Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));

                Total = TotalResult();
            }
        }
        public double Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public double TotalResult()
        {
            if (Cost == 0 || Quantity == 0)
            {
                return 0;
            }
            else
            {
                return Cost * Quantity;
            }
        }

        [RelayCommand]
        private void CancelLineItem()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                Description = string.Empty;
                Quantity = 0;
                Cost = 0;
                Total = 0;
            }
        }
        [RelayCommand]
        private void AddLineItem()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                ItemId = newLineItems.Count + 1;
                var newLineItem = new LineItemModel
                {
                    ItemId = ItemId,
                    Description = Description,
                    Quantity = Quantity,
                    Cost = Cost,
                    Total = Total
                };
                newLineItems.Add(newLineItem);

                Description = string.Empty;
                Quantity = 0;
                Cost = 0;
                Total = 0;
            }
        }
        [RelayCommand]
        private void GoBack()
        {
            foreach (var item in newLineItems)
            {
                _passingService.StoredItems.Add(item);
            }
            Navigation.NavigateTo<InvoiceViewModel>();
        }
    }
}
