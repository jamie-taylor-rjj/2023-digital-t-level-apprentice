using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Invoice_GenUI.Models
{
    public partial class CreateClientModel 
    {
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }
        public string? ContactName { get; set; } 
        public string? ContactEmail { get; set; }

        [RelayCommand]
        public void GoToClientDetails()
        {
            MessageBox.Show("Clicked");
        }
    }
}
