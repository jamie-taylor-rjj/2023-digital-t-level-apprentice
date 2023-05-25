using Invoice_GenUI.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Invoice_GenUI
{
    /// <summary>
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        private readonly InvoiceViewModel _viewModel;
        public InvoiceWindow(InvoiceViewModel viewModel) 
        {
            _viewModel = viewModel;
            
            InitializeComponent();

            txt_totalValue.Text += " " + _viewModel.LineItemsTotal().ToString();

            DataContext = _viewModel;

            dg_lineItems.ItemsSource = _viewModel.LineItems;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // -------------------------- NAVIGATION BUTTONS ----------------------------

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true; // When trying to show start up window = circular dependency
            this.Hide();
        }

        private void cb_clientName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clientBox = (ComboBox)sender;

            var selectedClient = (ClientNameViewModel)clientBox.SelectedItem;
            txt_reference.Text = "RJJ-" + selectedClient.ClientName;
        }

        private void dt_issueDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var input = dt_issueDate.SelectedDate.Value;

            var selectedDate = DateOnly.FromDateTime(input);

            txt_reference.Text += selectedDate.ToString();
        }
    }
}
