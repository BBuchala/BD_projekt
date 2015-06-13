using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Data.Entity;
using ProjektBD.Model;

namespace ProjektBD.Databases
{
    /// <summary>
    /// Baza danych dla formularza prowadzącego.
    /// </summary>
    class TeacherDatabase : UserDatabase
    {
        #region Konstruktor
        //----------------------------------------------------------------

        public TeacherDatabase(string teacherLogin)
        {
            userID = context.Prowadzący
                .Where( p => p.login.Equals(teacherLogin) )
                .Select( p => p.UżytkownikID )
                .Single();
        }

        //----------------------------------------------------------------
        #endregion

        #region Zgłoszenia
        //----------------------------------------------------------------

        /// <summary>
        /// Przeszukuje bazę danych pod katem zgłoszeń dla odpowiedniego nauczyciela
        /// </summary>
        /// <param name="teacherLogin">Login prowadzącego, dla którego szukamy zgłoszeń</param>
        /// <returns>Lista zgłoszeń na projekt w odpowiedniej formie</returns>
        internal List<ZgłoszenieNaProjektDTO> getProjectApplications(string teacherLogin)
        {
            var query = from u in context.Użytkownicy
                            join zg in context.Zgłoszenia on u.UżytkownikID equals zg.StudentID
                            join p in context.Projekty on zg.ProjektID equals p.ProjektID
                            join u2 in context.Użytkownicy on zg.ProwadzącyID equals u2.UżytkownikID
                            join s in context.Studenci on u.UżytkownikID equals s.UżytkownikID
                            join prz in context.Przedmioty on zg.PrzedmiotID equals prz.PrzedmiotID
                        where u2.login == teacherLogin &&
                            zg.jestZaakceptowane == false &&
                            zg.ProjektID.HasValue
                        select new ZgłoszenieNaProjektDTO
                        {
                            loginStudenta = u.login,
                            nazwaProjektu = p.nazwa,
                            numerIndeksu = s.nrIndeksu,
                            nazwaPrzedmiotu = prz.nazwa,
                            IDZgłoszenia = zg.ZgłoszenieID
                        };

            return query.ToList();
        }

        /// <summary>
        /// Przeszukuje bazę danych pod katem zgłoszeń dla odpowiedniego nauczyciela
        /// </summary>
        /// <param name="teacherLogin">Login prowadzącego, dla którego szukamy zgłoszeń</param>
        /// <returns>Lista zgłoszeń na przedmiot w odpowiedniej formie</returns>
        internal List<ZgłoszenieNaPrzedmiotDTO> getSubjectApplications(string teacherLogin)
        {
            var query = from u in context.Użytkownicy
                            join zg in context.Zgłoszenia on u.UżytkownikID equals zg.StudentID
                            join p in context.Przedmioty on zg.PrzedmiotID equals p.PrzedmiotID
                            join u2 in context.Użytkownicy on zg.ProwadzącyID equals u2.UżytkownikID
                            join s in context.Studenci on u.UżytkownikID equals s.UżytkownikID
                        where u2.login == teacherLogin &&
                            zg.jestZaakceptowane == false &&
                            zg.ProjektID.HasValue == false
                        select new ZgłoszenieNaPrzedmiotDTO
                        {
                            loginStudenta = u.login,
                            nazwaPrzedmiotu = p.nazwa,
                            numerIndeksu = s.nrIndeksu,
                            IDZgłoszenia = zg.ZgłoszenieID
                        };

            return query.ToList();
        }

        /// <summary>
        /// Wywala Zgłoszenie z bazy danych.
        /// </summary>
        /// <param name="applicationID">ID usuwanego zgłoszenia</param>
        public void deleteApplication(long applicationID)
        {
            Zgłoszenie AppToDelete = context.Zgłoszenia.Where(zg => zg.ZgłoszenieID == applicationID).FirstOrDefault();

            if (AppToDelete != null)
            {
                context.Zgłoszenia.Remove(AppToDelete);
                context.SaveChanges();
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Dodawanie do przedmiotu/projektu
        //----------------------------------------------------------------

        /// <summary>
        /// Dodaje studenta do przedmiotu.
        /// </summary>
        /// <param name="appl">Zgłoszenie, na podstawie którego zostaje przyęty.</param>
        public void addStudentToSubject(long applicationID)
        {
           
            Zgłoszenie appl = context.Zgłoszenia.Where(z => z.ZgłoszenieID == applicationID).Single();

            // Na wszelki wypadek lepiej zmieńmy to na student.Add(przedmiot)
            // W teorii zmiany dokonane przez ExecuteSqlCommand nie powinny być widoczne dla kontekstu,
            // więc nie wiadomo, czy kiedyś się to nie wykrzaczy
            var command = @"INSERT INTO Przedmioty_studenci VALUES (@param, @param2)";

            var teacherQuery = context.Database.ExecuteSqlCommand(command, new SqlParameter("param", appl.PrzedmiotID), new SqlParameter("param2",appl.StudentID));

            deleteApplication(applicationID);
        }

        /// <summary>
        /// Dodaje studenta do projektu.
        /// </summary>
        /// <param name="appl">Zgłoszenie, na podstawie którego zostaje przyęty.</param>
        public void addStudentToProject(long applicationID)
        {
            Zgłoszenie appl = context.Zgłoszenia.Where(z => z.ZgłoszenieID == applicationID).Single();

            var command = @"INSERT INTO Projekty_studenci VALUES (@param, @param2)";

            var teacherQuery = context.Database.ExecuteSqlCommand(command, new SqlParameter("param", appl.ProjektID), new SqlParameter("param2", appl.StudentID));

            deleteApplication(applicationID);
        }

        //----------------------------------------------------------------
        #endregion

        #region Raport

        public PrzedmiotRaportDetailsDTO getSubjectInfo(string subjectName)
        {
            var query = from subj in context.Przedmioty
                        join ob in context.PrzedmiotyObieralne on subj.PrzedmiotID equals ob.PrzedmiotID into obier

                        from allSubjects in obier.DefaultIfEmpty()         
                        where subj.nazwa.Equals(subjectName)
                        select new PrzedmiotRaportDetailsDTO
                        {
                            nazwa = subj.nazwa,
                            prowadzący = subj.Prowadzący.login,           
                            liczbaStudentów = subj.liczbaStudentów,
                            maxLiczbaStudentów = allSubjects.maxLiczbaStudentów,
                            liczbaProjektów = subj.Projekty.Count
                        };

            return query.Single();
        }

        public List<StudentDTO> getZestawienieOcen(string subjectName, string przedział1, string przedział2)
        {
            var query = context.Database.SqlQuery<StudentDTO>(@"
                            SELECT u.login
                            FROM Student s
	                            JOIN Użytkownik u ON u.UżytkownikID = s.UżytkownikID 
                                
	                            JOIN Przedmioty_studenci ps ON ps.StudentID = s.UżytkownikID
                                JOIN Przedmiot subj ON subj.PrzedmiotID = ps.PrzedmiotID
                                JOIN Ocena o ON o.PrzedmiotID = subj.PrzedmiotID AND o.StudentID = ps.StudentID
                              
                           WHERE subj.nazwa = '" + subjectName + @"'  
                            GROUP BY u.login 
                           HAVING AVG(o.wartość) >=" + przedział1 + @" AND AVG(o.wartość) < "+przedział2);

            return query.ToList();

        }
        public List<StudentDTO> getStudenciZProjektow(string projectName)
        {
            var studentQuery = context.Database.SqlQuery<StudentDTO>(@"
                            SELECT u.login
                            FROM Student s
	                            JOIN Użytkownik u ON u.UżytkownikID = s.UżytkownikID
	                            JOIN Projekty_studenci ps ON ps.StudentID = s.UżytkownikID
	                            JOIN Projekt proj ON proj.ProjektID = ps.ProjektID
                            WHERE proj.nazwa = '" + projectName + "'");

            return studentQuery.ToList();
        }

        public List<ProjektDTO> getProjekty(string subjectName)
        {
            var projectQuery = from proj in context.Projekty
                               join subj in context.Przedmioty on proj.PrzedmiotID equals subj.PrzedmiotID
                               where subj.nazwa.Equals(subjectName)
                               select new ProjektDTO { nazwa = proj.nazwa, maxLiczbaStudentów = proj.maxLiczbaStudentów };

            return projectQuery.ToList();
        }
    
        #endregion

        #region Pobieranie
        //----------------------------------------------------------------

        #region Przedmioty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera przedmioty prowadzącego z bazy
        /// </summary>
        public List<PrzedmiotProwadzącegoDTO> getMySubjects()
        {
            // Buguje się przy informatyce, nie mam pojęcia dlaczego
            //var query = from prow in context.Prowadzący
            //                join subj in context.Przedmioty on prow.UżytkownikID equals subj.ProwadzącyID
            //                join ob in context.PrzedmiotyObieralne on prow.UżytkownikID equals ob.ProwadzącyID into obier

            //            from obierki in obier.DefaultIfEmpty()
            //            where prow.UżytkownikID == userID
            //            select new PrzedmiotProwadzącegoDTO
            //            {
            //                nazwa = subj.nazwa,
            //                liczbaStudentów = subj.liczbaStudentów,
            //                maxLiczbaStudentów = obierki.maxLiczbaStudentów
            //            };

            var query = context.Database.SqlQuery<PrzedmiotProwadzącegoDTO>(@"
                                    SELECT p.nazwa, p.liczbaStudentów, ob.maxLiczbaStudentów
                                    FROM Przedmiot p
	                                    JOIN Prowadzący prow ON prow.UżytkownikID = p.ProwadzącyID
	                                    LEFT JOIN PrzedmiotObieralny ob ON ob.PrzedmiotID = p.PrzedmiotID
                                    WHERE prow.UżytkownikID = " + userID);

            return query.ToList();
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z bazy projekty studenta z podanego przedmiotu
        /// </summary>
        public List<ForeignProjektDTO> getStudentProjects(int studentIndexNumber, string subjectName)
        {
            // Nazwijcie mnie geniuszem... albo debilem... ale ***** DZIAŁA!!!
            var query = context.Database.SqlQuery<ForeignProjektDTO>(@"
                            SELECT p.nazwa, liczba AS liczbaStudentów, p.maxLiczbaStudentów
                            FROM Projekt p
                                JOIN Przedmiot subj ON subj.PrzedmiotID = p.PrzedmiotID
                                JOIN Projekty_studenci ps ON ps.ProjektID = p.ProjektID
                                JOIN Student stud ON stud.UżytkownikID = ps.StudentID
                                LEFT JOIN
                                (
		                            SELECT p.nazwa, COUNT(ps.ProjektID) AS liczba
		                            FROM Projekt p
			                            JOIN Przedmiot subj ON subj.PrzedmiotID = p.PrzedmiotID
			                            JOIN Projekty_studenci ps ON ps.ProjektID = p.ProjektID
		                            GROUP BY p.nazwa
                                ) AS countJoin ON p.nazwa = countJoin.nazwa
    
                            WHERE subj.nazwa = '" + subjectName + @"' AND
	                            stud.nrIndeksu = " + studentIndexNumber);

            return query.ToList();
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion
    }
}
