using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Falla_Amics.Models
{
    // Clase que representa un fallero con notificación de cambios para data-binding
    public class Fallero : INotifyPropertyChanged
    {
        // Campos privados para almacenar las propiedades
        private string? nombre;
        private string? apellidos;
        private string? contrasenya;
        private string? rol;
        private string? dni;
        private string? imagenPerfil;
        private string? genero;

        // Propiedad Nombre con notificación de cambios
        public string? Nombre
        {
            get => nombre;
            set
            {
                if (nombre != value)
                {
                    nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        // Propiedad Apellidos con formateo a "Title Case" y notificación de cambios
        public string? Apellidos
        {
            get => apellidos;
            set
            {
                if (value == null)
                {
                    if (apellidos != null)
                    {
                        apellidos = null;
                        OnPropertyChanged();
                    }
                }
                else
                {
                    // Formatear los apellidos para que cada palabra empiece con mayúscula
                    var formateado = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                    if (apellidos != formateado)
                    {
                        apellidos = formateado;
                        OnPropertyChanged();
                    }
                }
            }
        }

        // Propiedad Contraseña con notificación de cambios
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

        // Propiedad Rol con notificación de cambios
        public string? Rol
        {
            get => rol;
            set
            {
                if (rol != value)
                {
                    rol = value;
                    OnPropertyChanged();
                }
            }
        }

        // Propiedad DNI con notificación de cambios
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

        // Propiedad ImagenPerfil con notificación de cambios
        public string? ImagenPerfil
        {
            get => imagenPerfil;
            set
            {
                if (imagenPerfil != value)
                {
                    imagenPerfil = value;
                    OnPropertyChanged();
                }
            }
        }

        // Propiedad Género con notificación de cambios
        public string? Genero
        {
            get => genero;
            set
            {
                if (genero != value)
                {
                    genero = value;
                    OnPropertyChanged();
                }
            }
        }

        // Evento para notificar que una propiedad cambió (INotifyPropertyChanged)
        public event PropertyChangedEventHandler? PropertyChanged;

        // Método protegido para lanzar el evento PropertyChanged
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            // Si hay suscriptores, se notifica el cambio de la propiedad indicada
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
