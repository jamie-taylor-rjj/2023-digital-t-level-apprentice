﻿using Invoice_GenUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Invoice_GenUI
{
    /// <summary>
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        private readonly CreateClientWindow _clientWindow;
        private List<LineItemViewModel> _tempLineIems = new List<LineItemViewModel>();
        public ObservableCollection<ClientNameViewModel> ClientNames = new ObservableCollection<ClientNameViewModel>();
        public InvoiceWindow(CreateClientWindow clientWindow)
        {
            _clientWindow = clientWindow;
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

            DataContext = new InvoiceViewModel();

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
