using Invoice_GenUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        private List<LineItem> _tempLineIems = new List<LineItem>();
        public InvoiceWindow()
        {
            _tempLineIems.Add(new()
            {
                Description = Guid.NewGuid().ToString(),
                Quantity = 2,
                Cost = 9.99
            }); 

            InitializeComponent();

            var runningTotal = 0.0;

            foreach (var item in _tempLineIems)
            {
                runningTotal += item.Total();
            }

            txt_totalValue.Text += " " + runningTotal.ToString();

            dg_lineItems.ItemsSource = _tempLineIems;
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

        private void btn_createClient_Click(object sender, RoutedEventArgs e)
        {
            var clientWindow = new CreateClientWindow();
            clientWindow.Show();
            this.Hide();
        }

        private void cb_clientName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clientBox = (ComboBox)sender;

            var clientName = (ComboBoxItem)clientBox.SelectedItem;
            txt_reference.Text = "RJJ-" + clientName.Content.ToString();
        }

        private void dt_issueDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var input = dt_issueDate.SelectedDate.Value;

            var selectedDate = DateOnly.FromDateTime(input);

            txt_reference.Text += selectedDate.ToString();
        }
    }
}
