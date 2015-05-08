using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using ProjektBD.Model;

namespace ProjektBD.Databases
{
    class RegistrationDatabase : DatabaseBase
    {
        /// <summary>
        /// Tworzy studenta i dodaje go do bazy
        /// </summary>
        public void createStudentAccount(List<TextBoxBase> textFields, int numerIndeksu, bool czyPodanoDate, DateTime dataUrodzenia)
        {
            Student s = new Student
            {
                login = textFields[0].Text,
                hasło = textFields[1].Text,
                email = textFields[3].Text,
                miejsceZamieszkania = textFields[5].Text,
                nrIndeksu = numerIndeksu
            };

            if (czyPodanoDate == true)
                s.dataUrodzenia = dataUrodzenia;
            else s.dataUrodzenia = null;

            context.Studenci.Add(s);
            context.SaveChanges();
        }

        /// <summary>
        /// Posłanie wiadomości do admina. On akceptuje - stworzony zostaje prowadzący.
        /// </summary>
        public void notifyAdmin(List<TextBoxBase> textFields, bool czyPodanoDate, DateTime dataUrodzenia)
        {
            Użytkownik u = new Użytkownik
            {
                login = textFields[0].Text,
                hasło = textFields[1].Text,
                email = textFields[3].Text,
                miejsceZamieszkania = textFields[5].Text
            };

            if (czyPodanoDate == true)
                u.dataUrodzenia = dataUrodzenia;
            else u.dataUrodzenia = null;

            context.Użytkownicy.Add(u);
            context.SaveChanges();
        }

        /// <summary>
        /// Funkcja do sprawdzania powtórzeń (czy zajęty).
        /// Nick i adres email trzeba sprawdzić ze wszystkimi userami, indeks tylko ze studentami.
        /// </summary>
        /// <param name="student">Jeżeli user jest studentem, dodatkowo sprawdzamy indeks.</param>
        /// <returns>Zwraca nazwę powtarzającego się atrybutu lub "" jeżeli jest ok (nic się nie powtarza).</returns>
        public string isOccupied(bool student, string login, string email, int numerIndeksu)
        {
            String inputLogin = login;
            String inputEmail = email;

            context.Użytkownicy.Load();

            var query = context.Użytkownicy.Local.Where(s => (s.email.Equals(inputEmail)));

            if (query.Count() > 0)
                return "Email";

            query = context.Użytkownicy.Local.Where(s => (s.login.Equals(inputLogin)));

            if (query.Count() > 0)
                return "login";

            if (student)
            {
                query = context.Studenci.Local.Where(s => (s.nrIndeksu == numerIndeksu));

                if (query.Count() > 0)
                    return "nr indeksu";
            }

            return "";
        }
    }
}
