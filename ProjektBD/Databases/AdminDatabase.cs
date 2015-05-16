using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProjektBD.Model;

using System.Data.Entity.Infrastructure;
using System.Collections;
using System.Collections.ObjectModel;

namespace ProjektBD.Databases
{
    /// <summary>
    /// Baza danych dla formularza administratora
    /// </summary>
    class AdminDatabase : DatabaseBase
    {
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
            // Korzystamy z narzędzia ORM, dlatego jeśli zapytanie jest w miarę proste, lepiej starajmy się
            // korzystać z obiektowych mechanizmów

            //var command = @"DELETE FROM UŻYTKOWNIK WHERE UżytkownikID = @param";
            //context.Database.ExecuteSqlCommand(command, new SqlParameter("param", u.UżytkownikID));

            Użytkownik userToDelete = context.Użytkownik.Where(s => s.UżytkownikID == u.UżytkownikID).FirstOrDefault();

            if (userToDelete != null)
            {
                context.Użytkownik.Remove(userToDelete);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// "Zmienia" użytkownika w prowadzącego - usuwa użytkownika i dodaje go jako prowadzącego.
        /// Nadaje początkowy zakład: *nieznany*.
        /// </summary>
        /// <param name="u">Upgrade'owanmy użytkownik</param>
        internal void addTeacher(Użytkownik u)
        {
            if (context.Zakład.Where(z => (z.nazwa.Equals("*nieznany*"))).ToList().Count < 1)
            {
                Zakład z = new Zakład { nazwa = "*nieznany*" };

                context.Zakład.Add(z);
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

            if (u.dataUrodzenia != null)                // data urodzenia domyślnie jest null, dlatego else zbędny
                p.dataUrodzenia = u.dataUrodzenia;

            deleteUser(u);                              // usuwamy użytkownika już teraz, by zrobił miejsce prowadzącemu

            // Modyfikuje licznik autoinkrementacji klucza głównego UżytkownikID
            // Dzięki temu świeżo dodanemu użytkownikowi zostanie przydzielony ID tego, który przed chwilą usunęliśmy
            // W zasadzie niepotrzebne, ale za to tabela ładniej wygląda :3
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Użytkownik', RESEED, " + (u.UżytkownikID - 1) + ");");

            context.Prowadzący.Add(p);
            context.SaveChanges();
        }

        /// <summary>
        /// Pobiera nazwy tabel istniejących w bazie
        /// </summary>
        public List<string> getTableNames()
        {
            return context.Database.SqlQuery<string>("SELECT name FROM sys.tables ORDER BY name").ToList();
        }

        /// <summary>
        /// Pobiera wszystkie wiersze z tablicy o podanej nazwie
        /// </summary>
        public async Task< List<object> > getTableData(string tableName)
        {
            object kontekstObj = (object) context;
            PropertyInfo prop = kontekstObj.GetType().GetProperty(tableName);           // Sprawdza, czy własność podana przez parametr
                                                                                        // znajduje się w kontekście
            if (prop != null)
            {
                var wartość = prop.GetValue(kontekstObj);                               // Pobiera wartość tej własności

                List<object> list = (wartość as IEnumerable<object>).ToList();

                return list;
            }
                                                                                        // Własności nie ma w kontekście - potrzeba nam surowego SQL'a :<
            Type t = Type.GetType("ProjektBD.Databases.AdminDatabase+" + tableName);    // Typ klasy, do której zapisany zostanie wynik zapytania

            return await context.Database.SqlQuery(t, "SELECT * FROM " + tableName).ToListAsync();
        }

        //---------------------
        // Prywatne klasy pomocnicze do obsługi tablic, których nie ma w kontekście
        // (do nich zwracane są wyniki zapytań)
        //---------------------
        class Prowadzone_rozmowy
        {
            public int RozmowaID { get; set; }
            public int UżytkownikID { get; set; }
        }

        class Przedmioty_studenci
        {
            public int PrzedmiotID { get; set; }
            public int StudentID { get; set; }
        }
    }
}