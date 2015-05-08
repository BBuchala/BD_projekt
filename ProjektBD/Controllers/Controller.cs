using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektBD.Databases;
using ProjektBD.Utilities;

namespace ProjektBD.Controllers
{
    /// <summary>
    /// Abstrakcyjna klasa zawierająca pola i netody wspólne dla wszystkich kontrolerów
    /// </summary>
    abstract class Controller
    {
        /// <summary>
        /// Zarządza operacjami przeprowadzanymi na bazie danych
        /// </summary>
        protected DatabaseBase database;


        /// <summary>
        /// Inicjalizuje połączenie z bazą danych
        /// </summary>
        public bool connectToDatabase()
        {
            return database.connectToDB();
        }

        /// <summary>
        /// Pozbywa się utworzonego kontekstu
        /// </summary>
        public void disposeContext()
        {
            database.disposeContext();
        }

        /// <summary>
        /// Zwraca wartość określającą, czy połączenie z bazą przebiegło pomyślnie
        /// </summary>
        public bool connectionSuccessful()
        {
            return database.connectionSuccessful;
        }

        /// <summary>
        /// Sprawdza, czy baza jest w stanie naprawczym
        /// </summary>
        public void checkEmergencyMode()
        {
            database.checkEmergencyMode();
        }

        /// <summary>
        /// Zmienia stan bazy danych na przeciwny
        /// </summary>
        public void changeEmergencyMode()
        {
            database.changeEmergencyMode();
        }
    }
}
