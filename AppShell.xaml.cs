using Falla_Amics.Views;

namespace Falla_Amics
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RutasNavegacion();
        }

        private void RutasNavegacion()
        {
            // Registrar las rutas de navegacion
            Routing.RegisterRoute("MenuPrincipalPage", typeof(MenuPrincipalPage));
        }
    }
}
