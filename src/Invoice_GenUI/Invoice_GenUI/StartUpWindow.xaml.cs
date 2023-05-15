using System.Windows;
using System.Windows.Input;

namespace Invoice_GenUI
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();
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
            var clientWindow = new CreateClientWindow();
            clientWindow.Show();
            this.Hide();
        }

        private void btn_invoice_Click(object sender, RoutedEventArgs e)
        {
            var invoiceWindow = new InvoiceWindow();
            invoiceWindow.Show();
            this.Hide();
        }
    }
}
