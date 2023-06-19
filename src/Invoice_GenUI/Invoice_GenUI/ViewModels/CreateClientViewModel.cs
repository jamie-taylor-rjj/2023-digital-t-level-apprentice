using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public class CreateClientPostModel
    {
        public string ClientNameInput { get; set; }
        public string ContactNameInput { get; set; }
        public string ContactEmailInput { get; set; }
        public string ClientAddressInput { get; set; }
    }
    public partial class CreateClientViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;

        [Required]
        public string? ClientNameInput { get; set; }
        
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
