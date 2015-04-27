using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Model;

namespace ProjektBD.DAL
{
    // Przy każdej zmianie modelu usuwa i tworzy bazę danych na nowo
    class ProjektBDInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProjektBDContext>
    {
        protected override void Seed(ProjektBDContext context)
        {
            Administrator admin = new Administrator
            {
                login = "admin",
                hasło = "piwo123",
                email = "admin@projektBD.pl",
                dataUrodzenia = DateTime.Parse("2000-01-01"),
                miejsceZamieszkania = "Gliwice"
            };
            context.Administratorzy.Add(admin);                 // Dodaje admina do bazy
            context.SaveChanges();                              // Commit

            var zakłady = new List<Zakład>
            {
                new Zakład { nazwa = "Astrofiz" },
                new Zakład { nazwa = "Gastrofiz" },
                new Zakład { nazwa = "ZMiTAC" }
            };
            zakłady.ForEach( z => context.Zakłady.Add(z) );
            context.SaveChanges();

            var prowadzący = new List<Prowadzący>
            {
                new Prowadzący { login = "BLanuszny", hasło = "gagatki", email = "BLanuszny@projektBD.pl",      // ID = 2
                    dataUrodzenia = DateTime.Parse("1960-02-21"), ZakładID = 1 },
                new Prowadzący { login = "ASzydło", hasło = "PoŁapach!", email = "ASzydło@projektBD.pl",        // ID = 3
                    dataUrodzenia = DateTime.Parse("1950-07-14"), ZakładID = 2 },
                new Prowadzący { login = "Drabik", hasło = "układ cyfrowy", email = "Drabik@projektBD.pl",      // ID = 4
                    dataUrodzenia = DateTime.Parse("1955-12-23"), ZakładID = 3 }
            };
            prowadzący.ForEach( p => context.Prowadzący.Add(p) );
            context.SaveChanges();

            var studenci = new List<Student>
            {
                new Student { login = "Wuda", hasło = "jesteśmy zgubieni", email = "Wuda@student.projektBD.pl",
                    dataUrodzenia = DateTime.Parse("1993-10-24"), miejsceZamieszkania = "Mysłowice", nrIndeksu = 219891 },      // ID = 5

                new Student { login = "Issei", hasło = "oppai~", email = "Issei@student.projektBD.pl", nrIndeksu = 666666 },    // ID = 6

                new Student { login = "Ervelan", hasło = "oraoraora...ora!", email = "Ervelan@student.projektBD.pl",
                    dataUrodzenia = DateTime.Parse("1993-04-13"), miejsceZamieszkania = "Mysłowice", nrIndeksu = 219741 },      // ID = 7

                new Student { login = "Forczu", hasło = "kotori1", email = "SM6969@4chan.org", nrIndeksu = 219766,              // ID = 8
                    miejsceZamieszkania = "Rybnik" },

                new Student { login = "Korda", hasło = "pedał", email = "Korda@student.projektBD.pl", nrIndeksu = 219795,
                    miejsceZamieszkania = "NekoMikoMikołów" }
            };
            studenci.ForEach ( s => context.Studenci.Add(s) );
            context.SaveChanges();

            var przedmioty = new List<Przedmiot>
            {
                new Przedmiot { nazwa = "Informatyka", liczbaStudentów = 31, ProwadzącyID = 2 },
                new Przedmiot { nazwa = "Fizyka", liczbaStudentów = 31, ProwadzącyID = 3 },
                new Przedmiot { nazwa = "TUC", liczbaStudentów = 69, ProwadzącyID = 3 }
            };
            przedmioty.ForEach( p => context.Przedmioty.Add(p) );
            context.SaveChanges();

            PrzedmiotObieralny przedmObier = new PrzedmiotObieralny
            {
                nazwa = "Fizycznie inspirowane algorytmy informatyczne",
                liczbaStudentów = 7,
                maxLiczbaStudentów = 15,
                ProwadzącyID = 2
            };
            context.PrzedmiotyObieralne.Add(przedmObier);
            context.SaveChanges();

            var oceny = new List<Ocena>
            {
                new Ocena { PrzedmiotID = 1, StudentID = 5, wartość = 3.5, komentarz = "Nie ma GUI" },
                new Ocena { PrzedmiotID = 1, StudentID = 7, wartość = 5.0 },
                new Ocena { PrzedmiotID = 2, StudentID = 7, wartość = 2.0, komentarz = "Ściąga w kalkulatorze" },
                new Ocena { PrzedmiotID = 2, StudentID = 6, wartość = 3.0 }
            };
            oceny.ForEach( o => context.Oceny.Add(o) );
            context.SaveChanges();
            
            var rozmowy = new List<Rozmowa>
            {
                new Rozmowa { dataRozpoczęcia = DateTime.Parse("2015-01-18") },
                new Rozmowa { dataRozpoczęcia = DateTime.Now }
            };
            rozmowy.ForEach(r => context.Rozmowy.Add(r));
            context.SaveChanges();

            //----------------------------------------------------------------------
            // Jeden ze sposobów dodawania rozmowy do studenta - taki trochę na pałę

            List<Student> studentsList = context.Studenci.ToList();

            studentsList[0].Rozmowy.Add( rozmowy[0] );
            studentsList[1].Rozmowy.Add( rozmowy[1] );

            //----------------------------------------------------------------------

            var wiadomości = new List<Wiadomość>
            {
                new Wiadomość { dataWysłania = DateTime.Parse("2015-01-18 13:41:25"), nadawca = "Kirei",
                    treść = "Yorokobe", RozmowaID = 1 }
            };
            wiadomości.ForEach(w => context.Wiadomości.Add(w));
            context.SaveChanges();
        }
    }
}
