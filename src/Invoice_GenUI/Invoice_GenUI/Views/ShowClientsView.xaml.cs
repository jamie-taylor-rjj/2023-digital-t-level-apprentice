using System.Windows;
using System.Windows.Controls;

namespace Invoice_GenUI.Views
{
    public partial class ShowClientsView : UserControl
    {
        public ShowClientsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show(dg_clients.SelectedItems.ToString());
        }
    }
}
