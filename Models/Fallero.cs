using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Falla_Amics.Models
{
    public class Fallero : INotifyPropertyChanged
    {
        private string? nombre;
        private string? apellidos;
        private string? contrasenya;
        private string? rol;
        private string? dni;
        private string? imagenPerfil;

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
                    var formateado = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                    if (apellidos != formateado)
                    {
                        apellidos = formateado;
                        OnPropertyChanged();
                    }
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



        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
