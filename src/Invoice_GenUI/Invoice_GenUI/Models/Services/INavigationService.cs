namespace Invoice_GenUI.Models.Services
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
        void ParameterNavigateTo<T>(object parameter) where T : ViewModel;
    }
}
