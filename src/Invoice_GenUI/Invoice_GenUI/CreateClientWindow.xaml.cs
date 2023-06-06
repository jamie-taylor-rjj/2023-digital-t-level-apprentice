using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.ViewModels;

namespace Invoice_GenUI;


public partial class CreateClientWindow : Window
{
    private readonly CreateClientViewModel _viewModel;
    private readonly ClientService _clientService;
    public CreateClientWindow(CreateClientViewModel viewModel, ClientService clientService)
    {
        _viewModel = viewModel;
        _clientService = clientService;

        InitializeComponent();

        DataContext = _viewModel;
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
        }
        else
        {
            _clientService.EnterClient(txt_clientName.Text, txt_clientAddress.Text, txt_clientEmail.Text, txt_clientContact.Text);
        }

        btn_createClient.IsEnabled = true;

        
    }

    
}
