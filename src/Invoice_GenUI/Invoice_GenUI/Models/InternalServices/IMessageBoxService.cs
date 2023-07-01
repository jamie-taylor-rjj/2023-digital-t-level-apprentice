using System.Windows;

namespace Invoice_GenUI.Models.InternalServices
{
    public interface IMessageBoxService
    {
        MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage image);
        bool Confirm(string message);
    }
}
