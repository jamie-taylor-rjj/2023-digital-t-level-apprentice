using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class CreateClientViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;

        public CreateClientViewModel(INavigationService navService)
        {
            Navigation = navService;
        }
        [RelayCommand]
        public void GoToHome()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
