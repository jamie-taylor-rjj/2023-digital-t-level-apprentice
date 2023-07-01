using System.Windows;

namespace Invoice_GenUI.Models.InternalServices
{
    public interface IMessageBoxService
    {
        bool Confirm(string message);
        MessageBoxResult Failed(string message);
        MessageBoxResult Success(string message);
        MessageBoxResult ValidationError(string message);
        bool Warning(string message);
    }
}
