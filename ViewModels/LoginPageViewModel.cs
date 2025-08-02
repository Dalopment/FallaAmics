using System.Windows.Input;
using Falla_Amics.Models;
using Falla_Amics.Services;

namespace Falla_Amics.ViewModels
{
    public partial class LoginPageViewModel : BindableObject
    {
        // Campos privados
        private bool isPasswordVisible = false;
        private string? dni;
        private string? contrasenya;

        // Servicio para manejar Firebase
        private readonly FirebaseServices firebaseService;

        // Constructor
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Uso seguro en MAUI para todas las plataformas.")]
        public LoginPageViewModel()
        {
            firebaseService = new FirebaseServices();

            // Comandos para login y toggle de visibilidad de contraseña
            LoginCommand = new Command(async () => await LoginAsync());
            TogglePasswordCommand = new Command(TogglePasswordVisibility);
        }

        // Comandos públicos accesibles desde la vista
        public ICommand LoginCommand { get; }
        public ICommand TogglePasswordCommand { get; }

        // Propiedad que indica si la contraseña está visible o no
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Uso seguro en MAUI para todas las plataformas.")]
        public bool IsPasswordVisible
        {
            get => isPasswordVisible;
            set
            {
                if (isPasswordVisible != value)
                {
                    isPasswordVisible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PasswordToggleIcon)); // Actualiza el icono
                    OnPropertyChanged(nameof(IsPasswordHidden));    // Actualiza la visibilidad inversa
                }
            }
        }

        // Propiedad complementaria para bindear a la visibilidad "contraria"
        public bool IsPasswordHidden => !IsPasswordVisible;

        // Propiedad que devuelve el código del icono según el estado de visibilidad
        public string PasswordToggleIcon => IsPasswordVisible ? "\ue8f5" : "\ue8f4";

        // Método para alternar la visibilidad de la contraseña
        private void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }

        // Propiedad bindable para el DNI del usuario
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Uso seguro en MAUI para todas las plataformas.")]
        public string? Dni
        {
            get => dni;
            set
            {
                if (dni != value)
                {
                    dni = value;
                    OnPropertyChanged();
                }
            }
        }

        // Propiedad bindable para la contraseña del usuario
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Uso seguro en MAUI para todas las plataformas.")]
        public string? Contrasenya
        {
            get => contrasenya;
            set
            {
                if (contrasenya != value)
                {
                    contrasenya = value;
                    OnPropertyChanged();
                }
            }
        }

        // Método asincrónico que maneja el login
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Uso seguro en MAUI para todas las plataformas.")]
        private async Task LoginAsync()
        {
            try
            {
                // Validación básica de campos vacíos
                if (string.IsNullOrWhiteSpace(Dni) || string.IsNullOrWhiteSpace(Contrasenya))
                {
                    await Shell.Current.DisplayAlert("Error", "El DNI y la contraseña no pueden estar vacíos", "Aceptar");
                    return;
                }

                var dniUpper = Dni.Trim().ToUpper();

                // Consulta a Firebase para obtener el usuario por DNI
                var usuario = await firebaseService.ObtenerFalleroPorDni(dniUpper);

                if (usuario == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Usuario no encontrado", "Aceptar");
                    return;
                }

                // Validación de contraseña
                if (usuario.Contrasenya != Contrasenya)
                {
                    await Shell.Current.DisplayAlert("Error", "Contraseña incorrecta", "Aceptar");
                    return;
                }

                // Guardar la instancia del usuario actual para acceso global
                FalleroActual.Fallero = usuario;

                // Navegar a la página principal de la app
                await Shell.Current.GoToAsync("MenuPrincipalPage");
            }
            catch (Exception ex)
            {
                // Mostrar cualquier error ocurrido
                await Shell.Current.DisplayAlert("Error", $"Error: {ex.Message}", "Aceptar");
            }
        }
    }
}
