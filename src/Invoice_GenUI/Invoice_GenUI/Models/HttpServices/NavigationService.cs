using System;

namespace Invoice_GenUI.Models.Services
{
    public class NavigationService : ViewModel, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private ViewModel _currentView;
        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }
        public void NavigateTo<TViewModels>() where TViewModels : ViewModel
        {
            ViewModel viewmodel = _viewModelFactory.Invoke(typeof(TViewModels));
            CurrentView = viewmodel;
        }
    }
}
