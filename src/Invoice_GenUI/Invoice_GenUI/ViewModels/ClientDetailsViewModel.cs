using CommunityToolkit.Mvvm.ComponentModel;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class ClientDetailsViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigation;

        public ClientDetailsViewModel(INavigationService navService, object details)
        {
            _navigation = navService;
        }   
        public string Name { get; }
    }
}
