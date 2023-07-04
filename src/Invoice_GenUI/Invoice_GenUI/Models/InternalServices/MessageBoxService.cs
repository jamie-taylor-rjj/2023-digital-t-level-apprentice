using System.Windows;

namespace Invoice_GenUI.Models.InternalServices
{
    public class MessageBoxService : IMessageBoxService
    {
        public bool Confirm(string message)
        {
            bool confirm = false;
            var result = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                confirm = true;
            }
            return confirm;
        }
        public MessageBoxResult Success(string message)
        {
            return MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public MessageBoxResult Failed(string message)
        {
            return MessageBox.Show(message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public MessageBoxResult ValidationError(string message)
        {
            return MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public bool Warning(string message)
        {
            bool confirm = false;
            var result = MessageBox.Show(message, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                confirm = true;
            }
            return confirm;
        }
    }
}
