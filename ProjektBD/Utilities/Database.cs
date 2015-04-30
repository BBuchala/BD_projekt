using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using ProjektBD.DAL;
using ProjektBD.Model;
using System.Data.SqlClient;

namespace ProjektBD.Utilities
{
    /// <summary>
    /// Klasa do obsługi połączenia z bazą danych
    /// </summary>
    // W przyszłości rozbijemy ją jeszcze bardziej i tutaj będą znajdować się wyłącznie metody bazodanowe.
    // Każda formatka będzie miała swoją osobną klasę realizującą cele, do których została stworzona (np. klasa Logowanie i metody z tym związane).
    // Każda taka specjalistyczna klasa będzie korzystała z klasy DatabaseUtils.
    // Dzięki temu choć trochę oddzielimy widok od logiki aplikacji.
    public class DatabaseUtils
    {
        #region Pola i konstruktor
        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        ProjektBDContext context;

        /// <summary>
        /// Określa, czy udało się połączyć z bazą danych
        /// </summary>
        public bool connectionSuccessful = false;

        public DatabaseUtils()
        {
            context = new ProjektBDContext();
        }
        #endregion

        /// <summary>
        /// Łączy się z bazą danych i dokonuje jej rekonstrukcji, jeśli modele nie są zgodne
        /// </summary>
        public bool connectToDB()
        {
            bool shouldCloseForm = false;

            try
            {
                context.Database.Initialize(false);
                context.Użytkownicy.Load();                 // Wczytuje do lokalnej kolekcji wszystkich użytkowników (w tym studentów, prowadzących itp.)

                connectionSuccessful = true;
            }

            catch (System.Data.SqlClient.SqlException)
            {
                DialogResult connRetry = MessageBox.Show("Nastąpił błąd podczas próby połączenia z bazą danych.\n Upewnij się, czy nie jesteś połączony w innym miejscu. \n Spróbować ponownie?",
                                                       "Błąd połączenia",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Exclamation);
                if (connRetry == DialogResult.No)
                    shouldCloseForm = true;
                else
                    connectToDB();
            }

            //catch (System.Data.DataException)
            //{
            //    MessageBox.Show("Baza danych jest obecnie wyłączona. Proszę spróbować później", "Prace konserwacyjne",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    //backgroundWorker1.RunWorkerCompleted += (s, e) => Close();
            //}

            return shouldCloseForm;
        }

        #region Formularz logowania
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
        #endregion

        #region Formularz rejestracji
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
        /// Nick i adres email trzeba sprawdzić ze wszytskimi userami, indeks tylko ze studentami.
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
        #endregion

        #region Formularz administratora

        /// <summary>
        /// Zmienia aktualny stan bazy danych. Przechodzi w tryb naprawczy, jeśli był ustawiony normalny i vice versa
        /// </summary>
        public void changeEmergencyMode()
        {
            if (!EmergencyMode.isEmergency)
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    @"  ALTER DATABASE ProjektBD
                        SET EMERGENCY WITH ROLLBACK IMMEDIATE"          // Przełącza bazę w tryb naprawczy. Dostęp mają tylko najwyżsi admini,
                    );                                                  // w dodatku mogą oni jedynie SELECT'ować.
                                                                        // Dodatkowo rozłącza wszystkich userów i cofa niezacommitowane transakcje
                EmergencyMode.isEmergency = true;
            }

            else
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    @"  ALTER DATABASE ProjektBD
                        SET ONLINE"
                    );

                EmergencyMode.isEmergency = false;
            }
        }

        /// <summary>
        /// Wyszukuje listę użytkowników, którzy nie są ani Studentami ani Prowadzącymi
        /// (czyli Ci starający się o Prowadzącego)
        /// </summary>
        /// <returns>Zwraca listę userów (nie będących studentami ani prowadzącymi)</returns>
        internal List<Użytkownik> findUsers()
        {
            var query = context.Database.SqlQuery<Użytkownik>("SELECT * " +
                                                            "FROM UŻYTKOWNIK u FULL OUTER JOIN STUDENT s " +
                                                            "ON u.UżytkownikID = s.UżytkownikID " +
                                                            "FULL OUTER JOIN PROWADZĄCY p " +
                                                            "ON u.UżytkownikID = p.UżytkownikID " +
                                                            "WHERE s.nrIndeksu IS NULL AND p.ZakładID IS NULL AND u.UżytkownikID >	1").ToList();

            return query;
        }

        /// <summary>
        /// Wywala z bazy podanego usera.
        /// </summary>
        /// <param name="u">Użytkownik usuwany z bazy</param>
        internal void deleteUser(Użytkownik u)
        {
            var command = @"DELETE FROM UŻYTKOWNIK WHERE UżytkownikID = @param";

            context.Database.ExecuteSqlCommand(command, new SqlParameter("param", u.UżytkownikID));
            context.SaveChanges();
        }

        /// <summary>
        /// "Zmienia" użytkownika w prowadzącego - usuwa użytkownika i dodaje go jako prowadzącego.
        /// Nadaje początkowy zakład: *nieznany*.
        /// </summary>
        /// <param name="u">Upgrade'owanmy użytkownik</param>
        internal void addTeacher(Użytkownik u)
        {
            if (context.Zakłady.Where(z => (z.nazwa.Equals("*nieznany*"))).ToList().Count < 1)
            {
                Zakład z = new Zakład { nazwa = "*nieznany*" };

                context.Zakłady.Add(z);
                context.SaveChanges();
            }

            Prowadzący p = new Prowadzący
            {
                login = u.login,
                hasło = u.hasło,
                email = u.email,
                miejsceZamieszkania = u.miejsceZamieszkania,
                ZakładID = 4
            };

            if (u.dataUrodzenia != null)
                p.dataUrodzenia = u.dataUrodzenia;
            else p.dataUrodzenia = null;

            Prowadzący pr = context.Prowadzący.Where(x => x.UżytkownikID == p.UżytkownikID).FirstOrDefault();

            if (pr == null)
            {
                context.Prowadzący.Add(p);
                context.SaveChanges();
            }

        }

        #endregion

        /// <summary>
        /// Pozbywa się utworzonego kontekstu
        /// </summary>
        public void disposeContext()
        {
            if (context != null)
                context.Dispose();
        }
    }
}
