using System.Windows;
using System.Windows.Input;

namespace Invoice_GenUI
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        private readonly CreateClientWindow _clientWindow;
        private readonly InvoiceWindow _invoiceWindow;
        public StartUpWindow(InvoiceWindow invoiceWindow, CreateClientWindow clientWindow)
        {
            InitializeComponent();
            _invoiceWindow = invoiceWindow;
            _clientWindow = clientWindow;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // -------------------------- NAVIGATION BUTTONS ----------------------------

        private void btn_createClient_Click(object sender, RoutedEventArgs e)
        {
            _clientWindow.Show();
        }

        private void btn_invoice_Click(object sender, RoutedEventArgs e)
        {
            _invoiceWindow.Show();
        }
    }
}
