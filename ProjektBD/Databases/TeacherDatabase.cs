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

        #region Usuwanie
        //----------------------------------------------------------------

        /// <summary>
        /// Usuwa z bazy przedmiot o podanej nazwie
        /// </summary>
        public void removeSubject(string subjectName)
        {
            var subject = context.Przedmioty
                .Where( p => p.nazwa.Equals(subjectName) )
                .Single();

            context.Przedmioty.Remove(subject);
            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa z bazy projekt o podanej nazwie
        /// </summary>
        public void removeProject(string projectName)
        {
            var project = context.Projekty
                .Where( p => p.nazwa.Equals(projectName) )
                .Single();

            var grades = context.Oceny
                .Include("Projekt")
                .Where( o => o.ProjektID == project.ProjektID )
                .ToList();

            context.Oceny.RemoveRange(grades);

            context.Projekty.Remove(project);
            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa z bazy ocenę o podanym ID
        /// </summary>
        public void removeGrade(long gradeID)
        {
            Ocena grade = context.Oceny
                .Where( o => o.OcenaID == gradeID )
                .Single();

            context.Oceny.Remove(grade);
            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa z bazy studenta o podanym numerze indeksu
        /// </summary>
        public void removeStudent(string subjectName, string projectName, int studentIndexNumber)
        {
            Student student = context.Studenci
                .Where( s => s.nrIndeksu == studentIndexNumber )
                .Include("Projekty")
                .Single();

            Przedmiot subject = context.Przedmioty
                .Where( s => s.nazwa.Equals(subjectName) )
                .Include("Studenci")
                .Single();

            Projekt project = context.Projekty
                .Where(p => p.nazwa.Equals(projectName))
                .Include("Studenci")
                .SingleOrDefault();

            dynamic gradesList;

            // Usuwanie z projektu
            if (project != null)
            {
                gradesList = context.Oceny
                    .Where( o => o.ProjektID == project.ProjektID );

                project.Studenci.Remove(student);
            }

            // Usuwanie z przedmiotu i wszystkich projektów w jego ramach, na które był zapisany
            else
            {
                gradesList = context.Oceny                                  // Oceny z przedmiotu (i zarazem z projektów)
                    .Where( o => o.PrzedmiotID == subject.PrzedmiotID );

                var projectsList = context.Projekty                         // Projekty, na które był zapisany
                    .Where( p => p.PrzedmiotID == subject.PrzedmiotID );

                foreach (Projekt proj in projectsList)
                    student.Projekty.Remove(proj);                          // Usuwanie studenta z projektów

                subject.Studenci.Remove(student);                           // Usuwanie studenta z przedmiotu
            }

            context.Oceny.RemoveRange(gradesList);                          // Usuwanie ocen z projektu/przedmiotu
            context.SaveChanges();
        }

        //----------------------------------------------------------------
        #endregion

        #region Dodawanie
        //----------------------------------------------------------------

        /// <summary>
        /// Dodaje studentowi ocenę z podanego przedmiotu
        /// </summary>
        public void addSubjectGrade(string studentLogin, OcenaDetailsDTO grade)
        {
            Student stud = context.Studenci
                .Where( s => s.login.Equals(studentLogin) )
                .Single();

            Przedmiot subj = context.Przedmioty
                .Where( p => p.nazwa.Equals(grade.nazwaPrzedmiotu) )
                .Single();

            Ocena o = new Ocena
            {
                wartość = grade.wartość,
                komentarz = grade.komentarz,
                dataWpisania = DateTime.Now,
                Przedmiot = subj
            };

            stud.Oceny.Add(o);
            context.SaveChanges();
        }

        /// <summary>
        /// Dodaje studentowi ocenę z podanego projektu
        /// </summary>
        public void addProjectGrade(string studentLogin, OcenaDetailsDTO grade)
        {
            Student stud = context.Studenci
                .Where(s => s.login.Equals(studentLogin))
                .Single();

            Projekt proj = context.Projekty
                .Where( p => p.nazwa.Equals(grade.nazwaProjektu) )
                .Include("Przedmiot")
                .Single();

            Ocena o = new Ocena
            {
                wartość = grade.wartość,
                komentarz = grade.komentarz,
                dataWpisania = DateTime.Now,
                ProjektID = proj.ProjektID,
                PrzedmiotID = proj.Przedmiot.PrzedmiotID
            };

            stud.Oceny.Add(o);
            context.SaveChanges();
        }

        //----------------------------------------------------------------
        #endregion

        #region Modyfikowanie
        //----------------------------------------------------------------

        /// <summary>
        /// Modyfikuje podaną ocenę
        /// </summary>
        public void modifyGrade(long gradeID, double newValue, string newDesc)
        {
            Ocena grade = context.Oceny
                .Where( o => o.OcenaID == gradeID )
                .Single();

            grade.wartość = newValue;
            grade.komentarz = newDesc;

            context.SaveChanges();
        }

        //----------------------------------------------------------------
        #endregion
    }
}
