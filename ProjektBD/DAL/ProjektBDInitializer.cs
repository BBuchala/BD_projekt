using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.DAL
{
    // Przy każdej zmianie modelu usuwa i tworzy bazę danych na nowo
    class ProjektBDInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProjektBDContext>
    {
        protected override void Seed(ProjektBDContext context)
        {
            // Wygląda jak z kalkulatora wyborczego, ale w wersji ostatecznej w inicjalizatorze będzie tworzony tylko admin, więc będzie tego mniej.
            // Innej drogi do zrobienia tego niestety nie ma. Próbowałem.
            string salt = Encryption.generateSalt();
            string salt2 = Encryption.generateSalt();
            string salt3 = Encryption.generateSalt();
            string salt4 = Encryption.generateSalt();
            string salt5;

            Administrator admin = new Administrator
            {
                login = "admin",
                hasło = Encryption.HashPassword("piwo123", salt),
                sól = salt,
                email = "admin@projektBD.pl",
                dataUrodzenia = DateTime.Parse("2000-01-01"),
                miejsceZamieszkania = "Gliwice"
            };
            context.Administrator.Add(admin);                 // Dodaje admina do bazy
            context.SaveChanges();                              // Commit

            var zakłady = new List<Zakład>
            {
                new Zakład { nazwa = "*nieznany*" },
                new Zakład { nazwa = "Astrofiz" },
                new Zakład { nazwa = "Gastrofiz" },
                new Zakład { nazwa = "ZMiTAC" }
            };
            zakłady.ForEach( z => context.Zakład.Add(z) );
            context.SaveChanges();

            var prowadzący = new List<Prowadzący>
            {
                new Prowadzący {                                // ID = 2
                    login = "BLanuszny",
                    hasło = Encryption.HashPassword("gagatki", salt2),
                    sól = salt2,
                    email = "BLanuszny@projektBD.pl",
                    dataUrodzenia = DateTime.Parse("1960-02-21"),
                    ZakładID = 1 },

                new Prowadzący {                                // ID = 3
                    login = "ASzydło",
                    hasło = Encryption.HashPassword("PoŁapach!", salt3),
                    sól = salt3,
                    email = "ASzydło@projektBD.pl",
                    dataUrodzenia = DateTime.Parse("1950-07-14"),
                    ZakładID = 2 },

                new Prowadzący {                                // ID = 4
                    login = "Drabik",
                    hasło = Encryption.HashPassword("układ cyfrowy", salt4),
                    sól = salt4,
                    email = "Drabik@projektBD.pl",
                    dataUrodzenia = DateTime.Parse("1955-12-23"),
                    ZakładID = 3 }
            };
            prowadzący.ForEach( p => context.Prowadzący.Add(p) );
            context.SaveChanges();

            salt = Encryption.generateSalt();
            salt2 = Encryption.generateSalt();
            salt3 = Encryption.generateSalt();
            salt4 = Encryption.generateSalt();
            salt5 = Encryption.generateSalt();

            var studenci = new List<Student>
            {
                new Student {                                   // ID = 5
                    login = "Wuda",
                    hasło = Encryption.HashPassword("jesteśmy zgubieni", salt),
                    sól = salt,
                    email = "Wuda@student.projektBD.pl",
                    dataUrodzenia = DateTime.Parse("1993-10-24"),
                    miejsceZamieszkania = "Mysłowice",
                    nrIndeksu = 219891 },

                new Student {                                   // ID = 6
                    login = "Issei",
                    hasło = Encryption.HashPassword("oppai~", salt2),
                    sól = salt2,
                    email = "Issei@student.projektBD.pl",
                    nrIndeksu = 666666 },

                new Student {                                   // ID = 7
                    login = "Ervelan",
                    hasło = Encryption.HashPassword("YEEART!", salt3),
                    sól = salt3,
                    email = "Ervelan@student.projektBD.pl",
                    dataUrodzenia = DateTime.Parse("1993-04-13"),
                    miejsceZamieszkania = "Mysłowice",
                    nrIndeksu = 219741 },

                new Student {                                   // ID = 8
                    login = "Forczu",
                    hasło = Encryption.HashPassword("kotori1", salt4),
                    sól = salt4,
                    email = "SM6969@4chan.org",
                    nrIndeksu = 219766,
                    miejsceZamieszkania = "Rybnik" },

                new Student {                                   // ID = 9
                    login = "Korda",
                    hasło = Encryption.HashPassword("pedał", salt5),
                    sól = salt5,
                    email = "Korda@student.projektBD.pl",
                    nrIndeksu = 219795,
                    miejsceZamieszkania = "NekoMikoMikołów" }
            };
            studenci.ForEach ( s => context.Student.Add(s) );
            context.SaveChanges();

            var przedmioty = new List<Przedmiot>
            {
                new Przedmiot { nazwa = "Informatyka", liczbaStudentów = 31, ProwadzącyID = 2 },
                new Przedmiot { nazwa = "Fizyka", liczbaStudentów = 31, ProwadzącyID = 3 },
                new Przedmiot { nazwa = "TUC", liczbaStudentów = 69, ProwadzącyID = 3 }
            };
            przedmioty.ForEach( p => context.Przedmiot.Add(p) );
            context.SaveChanges();

            PrzedmiotObieralny przedmObier = new PrzedmiotObieralny
            {
                nazwa = "Fizycznie inspirowane algorytmy informatyczne",
                liczbaStudentów = 7,
                maxLiczbaStudentów = 15,
                ProwadzącyID = 2
            };
            context.PrzedmiotObieralny.Add(przedmObier);
            context.SaveChanges();

            var oceny = new List<Ocena>
            {
                new Ocena { PrzedmiotID = 1, StudentID = 5, wartość = 3.5, komentarz = "Nie ma GUI" },
                new Ocena { PrzedmiotID = 1, StudentID = 7, wartość = 5.0 },
                new Ocena { PrzedmiotID = 2, StudentID = 7, wartość = 2.0, komentarz = "Ściąga w kalkulatorze" },
                new Ocena { PrzedmiotID = 2, StudentID = 6, wartość = 3.0 }
            };
            oceny.ForEach( o => context.Ocena.Add(o) );
            context.SaveChanges();
            
            var rozmowy = new List<Rozmowa>
            {
                new Rozmowa { dataRozpoczęcia = DateTime.Parse("2015-01-18") },
                new Rozmowa { dataRozpoczęcia = DateTime.Now }
            };
            rozmowy.ForEach(r => context.Rozmowa.Add(r));
            context.SaveChanges();

            //----------------------------------------------------------------------
            // Jeden ze sposobów dodawania rozmowy do studenta - taki trochę na pałę

            List<Student> studentsList = context.Student.ToList();

            studentsList[0].Rozmowy.Add( rozmowy[0] );
            studentsList[1].Rozmowy.Add( rozmowy[1] );

            //----------------------------------------------------------------------

            var wiadomości = new List<Wiadomość>
            {
                new Wiadomość { dataWysłania = DateTime.Parse("2015-01-18 13:41:25"), nadawca = "Kirei",
                    treść = "Yorokobe", RozmowaID = 1 }
            };
            wiadomości.ForEach(w => context.Wiadomość.Add(w));
            context.SaveChanges();
        }
    }
}
