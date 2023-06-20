using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;

        public MainViewModel(INavigationService navService)
        {
            _navigation = navService;
        }
        [RelayCommand]
        public void GoToHome()
        {
            _navigation.NavigateTo<HomeViewModel>();
        }
    }
}
