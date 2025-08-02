using Falla_Amics.ViewModels;

namespace Falla_Amics.Views;

public partial class MenuPrincipalPage : ContentPage
{
	public MenuPrincipalPage()
	{
		InitializeComponent();
        BindingContext = new MenuPrincipalPageViewModel();
    }
}