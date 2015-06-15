using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using ProjektBD.Model;

namespace ProjektBD.Databases
{
    /// <summary>
    /// Baza danych dla formularza logowania
    /// </summary>
    class AccountDatabase : DatabaseBase
    {
        public string getUserSalt(string login)
        {
            return context.Użytkownicy
                .Where(u => u.login.Equals(login))
                .Select(s => s.sól)
                .FirstOrDefault();
        }

        /// <summary>
        ///     Wyszukuje użytkownika w bazie na podstawie podanego loginu i hasła
        /// </summary>
        /// <returns>
        ///     Obiekt użytkownika, jeśli logowanie przebiegło pomyślnie. False, jeśli użytkownik nie istnieje w bazie
        /// </returns>
        internal Użytkownik loginQuery(string login, string hashedPassword)
        {
            // FirstOrDefault zwraca pierwszy wynik zapytania lub null, jeśli użytkownik nie został znaleziony
            return context.Użytkownicy
                .Where( s => s.login.Equals(login) && s.hasło.Equals(hashedPassword) )
                .FirstOrDefault();
        }
    }
}
