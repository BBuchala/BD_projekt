using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Model;

namespace ProjektBD.Databases
{
    class AccountDatabase : DatabaseBase
    {
        /// <summary>
        ///     Wyszukuje użytkownika w bazie na podstawie podanego loginu i hasła
        /// </summary>
        /// <returns>
        ///     Obiekt użytkownika, jeśli logowanie przebiegło pomyślnie. False, jeśli użytkownik nie istnieje w bazie
        /// </returns>
        internal Użytkownik loginQuery(string login, string password)
        {
            // FirstOrDefault zwraca pierwszy wynik zapytania lub null, jeśli użytkownik nie został znaleziony
            return context.Użytkownicy.Where(s => (s.login.Equals(login) && s.hasło.Equals(password))).FirstOrDefault();
        }
    }
}
