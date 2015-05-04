using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Forms;
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Controllers
{
    class AccountController
    {
        /// <summary>
        /// Zarządza operacjami przeprowadzanymi na bazie danych
        /// </summary>
        private DatabaseUtils database;

        public AccountController()
        {
            database = new DatabaseUtils();
        }
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
        /// Sprawdza, czy w bazie istnieje użytkownik o podanym loginie i haśle.
        /// Zwraca rodzaj użytkownika lub null w przypadku niepowodzenia
        /// </summary>
        public string validateUser(string login, string password)
        {
            Użytkownik query = database.loginQuery(login, password);

            if (query != null)
                return query.GetType().Name;
            else
                return "";
        }

        /// <summary>
        /// Sprawdza, czy baza nie jest w stanie naprawczym.
        /// Jeśli jest, zwraca null.
        /// Jeśli nie, zwraca typ formularza zgodny z uprawnieniami użytkownika.
        /// </summary>
        public Form openUserForm(string userType)
        {
            EmergencyMode.checkEmergencyMode();

            if ( EmergencyMode.isEmergency && !userType.Equals("Administrator") )
            {
                return null;
            }
            else
            {
                switch (userType)
                {
                    case "Administrator":
                        return new AdministratorMain();

                    case "Prowadzący":
                        return new ProwadzacyMain();

                    case "Student":
                        return new StudentMain();

                    default:
                        return new Form();
                }
            }
        }
    }
}