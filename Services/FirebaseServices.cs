using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Falla_Amics.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace Falla_Amics.Services
{
    /// <summary>
    /// Servicio que gestiona la comunicación con Firebase Realtime Database.
    /// </summary>
    public class FirebaseServices
    {
        // Cliente de Firebase que se conecta a la base de datos.
        private readonly FirebaseClient firebaseConexion;

        /// <summary>
        /// Constructor que inicializa la conexión con Firebase usando la URL del proyecto.
        /// </summary>
        public FirebaseServices()
        {
            string firebaseUrl = "https://amicsnaquera-default-rtdb.firebaseio.com/";
            firebaseConexion = new FirebaseClient(firebaseUrl);
        }

        /// <summary>
        /// Obtiene un objeto Fallero desde Firebase usando el DNI como clave.
        /// </summary>
        /// <param name="dni">DNI del fallero a buscar.</param>
        /// <returns>Objeto Fallero correspondiente al DNI proporcionado.</returns>
        public async Task<Fallero> ObtenerFalleroPorDni(string dni)
        {
            return await firebaseConexion
                .Child("Falleros")            // Accede al nodo "Falleros" de la base de datos.
                .Child(dni)                  // Accede al subnodo con clave igual al DNI.
                .OnceSingleAsync<Fallero>(); // Recupera el objeto Fallero de forma asíncrona.
        }
    }
}
