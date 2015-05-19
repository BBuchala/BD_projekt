using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using ProjektBD.Model;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using ProjektBD.Utilities;

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

            string salt = Encryption.generateSalt();
            string hashedPassword = Encryption.HashPassword(u.hasło, salt);

            Prowadzący p = new Prowadzący
            {
                login = u.login,
                hasło = hashedPassword,
                sól = salt,
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
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Użytkownik', RESEED, " + (u.UżytkownikID - 1) + ");");

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
        /// Pobiera nazwy zakładów istniejących w bazie
        /// </summary>
        public List<string> getInstituteNames()
        {
            return context.Zakłady.Select(n => n.nazwa).ToList();
        }

        /// <summary>
        /// Pobiera wszystkie wiersze z tabeli o podanej nazwie
        /// </summary>
        /// <typeparam name="T">Typ encji, której elementy przechowywane będą w zwracanej liscie</typeparam>
        public IList getTableData<T>(string tableName) where T: class
        {
            if ( tableName.Equals("Prowadzone_rozmowy") )
            {
                return context.Database.SqlQuery<Prowadzone_rozmowy>("SELECT * FROM Prowadzone_rozmowy").ToList();
            }
            else if ( tableName.Equals("Przedmioty_studenci") )
            {
                return context.Database.SqlQuery<Przedmioty_studenci>("SELECT * FROM Przedmioty_studenci").ToList();
            }
            else
            {
                DbSet<T> result = context.Set<T>();         // Tworzy nowy DbSet, który podpiernicza interesujące nas elementy z istniejącego kontekstu
                result.Load();
                return result.Local.ToBindingList();
            }
        }

        /// <summary>
        /// Pobiera nazwy kluczy głównych z tabeli
        /// </summary>
        /// <typeparam name="T">Typ klasy encji, której klucza szukamy</typeparam>
        public List<string> getPrimaryKeyNames<T>() where T : class
        {
            var set = ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<T>();
            var entitySet = set.EntitySet;

            return entitySet.ElementType.KeyMembers.Select(k => k.Name).ToList();
        }

        /// <summary>
        /// Sprawdza, czy kontekst posiada nowe dane, które musi wysłać do bazy
        /// </summary>
        public bool doesContextHaveChanges()
        {
            return context.ChangeTracker.HasChanges();
        }

        /// <summary>
        /// Pobiera prowadzących z bazy
        /// </summary>
        public List<Prowadzący> getTeachers()
        {
            var annonymousQuery = context.Prowadzący.Select(a => new { a.UżytkownikID, a.login, a.email, a.miejsceZamieszkania, a.dataUrodzenia }).ToList();
            var teacherQuery = annonymousQuery.Select(p => new Prowadzący
            {
                UżytkownikID = p.UżytkownikID,
                login = p.login,
                email = p.email,
                miejsceZamieszkania = p.miejsceZamieszkania,
                dataUrodzenia = p.dataUrodzenia
            });


            return teacherQuery.ToList();
        }
    }
}