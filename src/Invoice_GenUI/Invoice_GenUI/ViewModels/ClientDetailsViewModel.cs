using System.Threading.Tasks;
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
        private ShowClientsViewModel _showClientsViewModel;

        public ClientDetailsViewModel(INavigationService navService, ShowClientsViewModel showClientsViewModel)
        {
            _navigation = navService;
            _showClientsViewModel = showClientsViewModel;
        }   

        [RelayCommand]
        private void GoBack()
        {
            _navigation.NavigateTo<ShowClientsViewModel>();
        }
    }
}
