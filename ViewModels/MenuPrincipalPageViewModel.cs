using System.Collections.ObjectModel;
using System.ComponentModel;
using Falla_Amics.Models;

namespace Falla_Amics.ViewModels
{
    // ViewModel para la página principal
    public class MenuPrincipalPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Fallero actual (puedes ampliar para OnPropertyChanged si cambias)
        public Fallero Fallero => FalleroActual.Fallero;

        // Mensajes de bienvenida
        public string MensajeBienvenida => $"Hola, {Fallero?.Nombre}";

        public string MensajeBienvenidaPersonalizado
        {
            get
            {
                if (Fallero == null)
                    return "Benvingut/Benvinguda a la Falla Amics de Nàquera";

                if (Fallero.Genero == "Hombre")
                    return "Benvingut a la Falla Amics de Nàquera";
                else if (Fallero.Genero == "Mujer")
                    return "Benvinguda a la Falla Amics de Nàquera";
                else
                    return "Benvingut/Benvinguda a la Falla Amics de Nàquera";
            }
        }

    }
}
