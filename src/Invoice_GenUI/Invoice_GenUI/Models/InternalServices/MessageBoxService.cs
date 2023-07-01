using System.Windows;

namespace Invoice_GenUI.Models.InternalServices
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage image)
        {
            return MessageBox.Show(message, caption, button, image);
        }
        public bool Confirm(string message)
        {
            bool confirm = false;
            var result = Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                confirm = true;
            }
            return confirm;
        }
    }
}
