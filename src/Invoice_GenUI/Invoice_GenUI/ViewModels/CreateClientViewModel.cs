using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Invoice_GenUI.ViewModels;

public partial class CreateClientViewModel : ObservableObject
{
    [ObservableProperty]
    private bool validating;

    [RelayCommand]
    public async Task ClientValidation()
    {
        Validating = true;

        MessageBox.Show("LOL");



        Validating = false;
    }
}
