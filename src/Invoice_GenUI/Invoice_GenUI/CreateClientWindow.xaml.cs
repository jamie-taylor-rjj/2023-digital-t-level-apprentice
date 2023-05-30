using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

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

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
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
