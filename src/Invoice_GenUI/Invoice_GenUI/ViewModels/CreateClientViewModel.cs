using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Invoice_GenUI.ViewModels;

public partial class CreateClientViewModel : ObservableObject
{
    [ObservableProperty]
    private bool validating;

    public async Task ClientValidation()
    {
        Validating = true;

        



        Validating = false;
    }
}
