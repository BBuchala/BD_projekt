using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektBD.Model;

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

            Użytkownik userToDelete = context.Użytkownicy.Where(s => s.UżytkownikID == u.UżytkownikID).FirstOrDefault();

            if (userToDelete != null)
            {
                context.Użytkownicy.Remove(userToDelete);
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
    }
}
