using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.InternalServices;
using Invoice_GenUI.Models.PassingValuesServices;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class AddLineItemViewModel : ViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IPassingService _passingService;
        private readonly IMessageBoxService _messageBoxService;

        public ObservableCollection<LineItemModel> newLineItems { get; } = new ObservableCollection<LineItemModel>();

        public AddLineItemViewModel(INavigationService navService, IPassingService passingService, IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
            _passingService = passingService;
            _navigation = navService;
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Field is required")]
        private string? _description;

        private int _quantity;
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                Total = TotalResult();
            }
        }
        private double _cost;
        [Required(ErrorMessage = "Field is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public double Cost
        {
            get => _cost;
            set
            {
                SetProperty(ref _cost, value);
                Total = TotalResult();
            }
        }
        private double _total;
        public double Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        public double TotalResult()
        {
            return Cost * Quantity;
        }

        [RelayCommand]
        private void CancelLineItem()
        {
            var result = _messageBoxService.Confirm("Are you sure?");
            if (result == true)
            {
                Description = string.Empty;
                Quantity = 0;
                Cost = 0;
                Total = 0;
            }
        }
        public int ItemId;
        [RelayCommand]
        private void AddLineItem()
        {
            var result = _messageBoxService.Confirm("Do you want to add this line item?");
            if (result == true)
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
                _passingService.StoredItems!.Add(item);
            }
            _navigation.NavigateTo<InvoiceViewModel>();
        }
    }
}
