using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ClientDetailsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;

        public ClientDetailsViewModel(INavigationService navService)
        {
            _navigation = navService;
        }   
        public string Name { get; }

        [RelayCommand]
        public void GoBack()
        {
            _navigation.NavigateTo<ShowClientsViewModel>();
        }
    }
}
