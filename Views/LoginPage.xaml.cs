using Falla_Amics.ViewModels;

namespace Falla_Amics.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
#pragma warning disable CA1416 // Validate platform compatibility
        BindingContext = new LoginPageViewModel(); // ya no necesitas "ViewModels." porque usas el using correcto
    }
}
