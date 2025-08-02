using System.Windows.Input;
using Falla_Amics.Models;
using Falla_Amics.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Falla_Amics.ViewModels
{
    public partial class LoginPageViewModel : BindableObject
    {
        private bool isPasswordVisible = false;
        private string? dni;
        private string? contrasenya;
        private bool recordarContrasenya;

        private readonly FirebaseServices firebaseService;

        public LoginPageViewModel()
        {
            firebaseService = new FirebaseServices();

            LoginCommand = new Command(async () => await LoginAsync());
            TogglePasswordCommand = new Command(TogglePasswordVisibility);

            _ = CargarDatosGuardadosAsync();
        }

        public ICommand LoginCommand { get; }
        public ICommand TogglePasswordCommand { get; }

        public bool IsPasswordVisible
        {
            get => isPasswordVisible;
            set
            {
                if (isPasswordVisible != value)
                {
                    isPasswordVisible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PasswordToggleIcon));
                    OnPropertyChanged(nameof(IsPasswordHidden));
                }
            }
        }

        public bool IsPasswordHidden => !IsPasswordVisible;
        public string PasswordToggleIcon => IsPasswordVisible ? "\ue8f5" : "\ue8f4";

        private void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }

        public bool RecordarContrasenya
        {
            get => recordarContrasenya;
            set
            {
                if (recordarContrasenya != value)
                {
                    recordarContrasenya = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private async Task LoginAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Dni) || string.IsNullOrWhiteSpace(Contrasenya))
                {
                    await Shell.Current.DisplayAlert("Error", "El DNI y la contraseña no pueden estar vacíos", "Aceptar");
                    return;
                }

                var dniUpper = Dni.Trim().ToUpper();
                var usuario = await firebaseService.ObtenerFalleroPorDni(dniUpper);

                if (usuario == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Usuario no encontrado", "Aceptar");
                    return;
                }

                if (usuario.Contrasenya != Contrasenya)
                {
                    await Shell.Current.DisplayAlert("Error", "Contraseña incorrecta", "Aceptar");
                    return;
                }

                FalleroActual.Fallero = usuario;

                if (RecordarContrasenya)
                {
                    Preferences.Set("Dni", Dni);
                    Preferences.Set("Contrasenya", Contrasenya);
                    Preferences.Set("RecordarContrasenya", true);
                }
                else
                {
                    Preferences.Remove("Dni");
                    Preferences.Remove("Contrasenya");
                    Preferences.Set("RecordarContrasenya", false);
                }

                await Shell.Current.GoToAsync("MenuPrincipalPage");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error: {ex.Message}", "Aceptar");
            }
        }

        private async Task CargarDatosGuardadosAsync()
        {
            try
            {
                Dni = Preferences.Get("Dni", string.Empty);
                Contrasenya = Preferences.Get("Contrasenya", string.Empty);
                RecordarContrasenya = Preferences.Get("RecordarContrasenya", false);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al cargar datos guardados: {ex.Message}", "Aceptar");
            }
        }
    }
}
