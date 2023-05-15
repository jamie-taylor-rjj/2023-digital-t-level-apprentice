using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invoice_GenUI
{
    /// <summary>
    /// Interaction logic for CreateClientWindow.xaml
    /// </summary>
    public partial class CreateClientWindow : Window
    {
        public CreateClientWindow()
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

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            var HomeWindow = new StartUpWindow();
            HomeWindow.Show();
            this.Hide();
        }

        private void btn_invoice_Click(object sender, RoutedEventArgs e)
        {
            var invoiceWindow = new InvoiceWindow();
            invoiceWindow.Show();
            this.Hide();
        }

        private void btn_createClient_Click(object sender, RoutedEventArgs e)
        {
            btn_createClient.IsEnabled = false;

            if (string.IsNullOrWhiteSpace(txt_clientName.Text) ||
                string.IsNullOrWhiteSpace(txt_clientAddress.Text) ||
                string.IsNullOrWhiteSpace(txt_clientContact.Text) ||
                string.IsNullOrWhiteSpace(txt_clientEmail.Text))
            {
                MessageBox.Show("Invalid data");
                btn_createClient.IsEnabled = true;

                return;
            }
            MessageBox.Show("Saved Client");
            btn_createClient.IsEnabled = true;
        }
    }

}
