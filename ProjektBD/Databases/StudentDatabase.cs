using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using ProjektBD.Model;

namespace ProjektBD.Databases
{
    class StudentDatabase : DatabaseBase
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        // TODO:
        // - zamiast joinować, w LINQ wykorzystać navigation properties

        public StudentDatabase(string studentName)
        {
            userID = context.Użytkownicy
                .Where( u => u.login.Equals(studentName) )
                .Select( u => u.UżytkownikID )
                .Single();
        }

        //----------------------------------------------------------------
        #endregion

        #region Pobieranie
        //----------------------------------------------------------------

        #region Użytkownicy
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z bazy listę użytkowników, których login zawiera w sobie podane słowo
        /// </summary>
        public List<UżytkownikDTO> getUser(string loginFragment)
        {
            var userQuery = from user in context.Użytkownicy
                            where user.login.Contains(loginFragment) &&
                                !user.login.Equals("admin")
                            select new UżytkownikDTO {
                                login = user.login,
                                stanowisko = (user is Student)? "Student" : "Prowadzący"
                            };

            return userQuery.ToList();
        }

        //----------------------------------------------------------------
        #endregion

        #region Przedmioty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera przedmioty z bazy
        /// </summary>
        public List<PrzedmiotDTO> getSubjects()
        {
            var teacherQuery =  from subj in context.Przedmioty
                                    join prow in context.Prowadzący on subj.ProwadzącyID equals prow.UżytkownikID
                                select new PrzedmiotDTO { nazwa = subj.nazwa, prowadzący = prow.login };

            return teacherQuery.ToList();
        }

        /// <summary>
        /// Pobiera przedmioty studenta z bazy
        /// </summary>
        public List<PrzedmiotDTO> getMySubjects()
        {
            var teacherQuery = context.Database.SqlQuery<PrzedmiotDTO>(@"
                            SELECT p.nazwa, prow.login AS prowadzący
                            FROM Przedmiot p
	                            JOIN Użytkownik prow ON prow.UżytkownikID = p.ProwadzącyID
	                            JOIN Przedmioty_studenci ps ON ps.PrzedmiotID = p.PrzedmiotID
                            WHERE ps.StudentID = " + userID);

            return teacherQuery.ToList();
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty

        /// <summary>
        /// Pobiera projekty realizowane w ramach przedmiotu
        /// </summary>
        public List<ProjektDTO> getProjects(string subjectName)
        {
            var projectQuery =  from proj in context.Projekty
                                    join subj in context.Przedmioty on proj.PrzedmiotID equals subj.PrzedmiotID
                                where subj.nazwa.Equals(subjectName)
                                select new ProjektDTO { nazwa = proj.nazwa, maxLiczbaStudentów = proj.maxLiczbaStudentów };

            return projectQuery.ToList();
        }

        /// <summary>
        /// Pobiera projekty użytkownika realizowane w ramach przedmiotu
        /// </summary>
        public List<ProjektDTO> getMyProjects(string subjectName)
        {
            var projectQuery = context.Database.SqlQuery<ProjektDTO>(@"
                            SELECT p.nazwa, p.maxLiczbaStudentów
                            FROM Projekt p
	                            JOIN Przedmiot subj ON subj.PrzedmiotID = p.PrzedmiotID
	                            JOIN Projekty_studenci ps ON ps.ProjektID = p.ProjektID
                            WHERE subj.nazwa = '" + subjectName + @"' AND
                                ps.StudentID = " + userID);

            return projectQuery.ToList();
        }

        /// <summary>
        /// Pobiera z bazy projekty realizowane w ramach przedmiotu, na które nie jest zapisany student
        /// </summary>
        public List<ForeignProjektDTO> getNotMyProjects(string subjectName)
        {
            var projectQuery = context.Database.SqlQuery<ForeignProjektDTO>(@"
                            SELECT p.nazwa, COUNT(ps.ProjektID) AS liczbaStudentów, p.maxLiczbaStudentów
                            FROM Projekt p
                                JOIN Przedmiot subj ON subj.PrzedmiotID = p.PrzedmiotID
                                LEFT JOIN Projekty_studenci ps ON ps.ProjektID = p.ProjektID
                            WHERE subj.nazwa = '" + subjectName + @"' AND
	                            p.nazwa NOT IN
	                            (
		                            SELECT p.nazwa
		                            FROM Projekt p
			                            JOIN Projekty_studenci ps ON ps.ProjektID = p.ProjektID
		                            WHERE ps.StudentID = " + userID + @"
                                )
                            GROUP BY p.nazwa, p.maxLiczbaStudentów
                            HAVING COUNT(ps.ProjektID) < p.maxLiczbaStudentów");

            return projectQuery.ToList();
        }

        #endregion

        #region Studenci
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera studentów zapisanych na przedmiot
        /// </summary>
        public List<StudentDTO> getStudentsFromSubject(string subjectName)
        {
            var studentQuery = context.Database.SqlQuery<StudentDTO>(@"
                            SELECT s.nrIndeksu, u.login, u.email
                            FROM Student s
	                            JOIN Użytkownik u ON u.UżytkownikID = s.UżytkownikID
	                            JOIN Przedmioty_studenci ps ON ps.StudentID = s.UżytkownikID
	                            JOIN Przedmiot subj ON subj.PrzedmiotID = ps.PrzedmiotID
                            WHERE subj.nazwa = '" + subjectName + "'");

            return studentQuery.ToList();
        }

        /// <summary>
        /// Pobiera studentów zapisanych na projekt z danego przedmiotu
        /// </summary>
        public List<StudentDTO> getStudentsFromProject(string projectName)
        {
            var studentQuery = context.Database.SqlQuery<StudentDTO>(@"
                            SELECT s.nrIndeksu, u.login, u.email
                            FROM Student s
	                            JOIN Użytkownik u ON u.UżytkownikID = s.UżytkownikID
	                            JOIN Projekty_studenci ps ON ps.StudentID = s.UżytkownikID
	                            JOIN Projekt proj ON proj.ProjektID = ps.ProjektID
                            WHERE proj.nazwa = '" + projectName + "'");

            return studentQuery.ToList();
        }

        #endregion

        #region Oceny
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera oceny z podanego przedmiotu
        /// </summary>
        public List<OcenaDTO> getGradesFromSubject(string subjectName)
        {
            var gradeQuery = from o in context.Oceny
                                join subj in context.Przedmioty on o.PrzedmiotID equals subj.PrzedmiotID
                                join user in context.Studenci on o.StudentID equals user.UżytkownikID
                            where user.UżytkownikID == userID &&
                                subj.nazwa.Equals(subjectName)
                            select new OcenaDTO {
                                nazwaProjektu = o.Projekt.nazwa,
                                wartość = o.wartość,
                                dataWpisania = o.dataWpisania,
                                ocenaID = o.OcenaID
                            };

            return gradeQuery.ToList();
        }

        /// <summary>
        /// Pobiera oceny z podanego projektu
        /// </summary>
        public List<OcenaZProjektuDTO> getGradesFromProject(string projectName)
        {
            var gradeQuery = from o in context.Oceny
                                join proj in context.Projekty on o.ProjektID equals proj.ProjektID
                                join user in context.Studenci on o.StudentID equals user.UżytkownikID
                             where user.UżytkownikID == userID &&
                                 proj.nazwa.Equals(projectName)
                             select new OcenaZProjektuDTO
                             {
                                 wartość = o.wartość,
                                 dataWpisania = o.dataWpisania,
                                 ocenaID = o.OcenaID
                             }; 

            return gradeQuery.ToList();
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Zapisywanie
        //----------------------------------------------------------------

        /// <summary>
        /// Zapisuje studenta na przedmiot o podanej nazwie.
        /// </summary>
        public void enrollToSubject(string subjectName)
        {
            Student stud = context.Studenci.Where( s => s.UżytkownikID == userID ).Single();
            Przedmiot subj = context.Przedmioty.Where( p => p.nazwa.Equals(subjectName) ).Single();

            Zgłoszenie z = new Zgłoszenie
            {
                StudentID = stud.UżytkownikID,
                PrzedmiotID = subj.PrzedmiotID,
                ProwadzącyID = subj.ProwadzącyID,
                jestZaakceptowane = false
            };

            context.Zgłoszenia.Add(z);
            context.SaveChanges();
        }

        /// <summary>
        /// Zapisuje studenta na projekt o podanej nazwie
        /// </summary>
        public void enrollToProject(string projectName)
        {
            Student stud = context.Studenci.Where( s => s.UżytkownikID == userID ).Single();
            Projekt proj = context.Projekty.Where( p => p.nazwa.Equals(projectName) ).Single();
            Przedmiot prz = context.Przedmioty.Where(pr => pr.PrzedmiotID == proj.PrzedmiotID).Single();

            Zgłoszenie z = new Zgłoszenie
            {
                StudentID = stud.UżytkownikID,
                ProjektID = proj.ProjektID,
                PrzedmiotID = proj.PrzedmiotID,
                ProwadzącyID = prz.ProwadzącyID,
                jestZaakceptowane = false
            };

            context.Zgłoszenia.Add(z);
            context.SaveChanges();
        }

        //----------------------------------------------------------------
        #endregion

        #region Metody pomocnicze
        //----------------------------------------------------------------

        /// <summary>
        /// Sprawdza, czy student nie wysłał już zgłoszenia na podany projekt.
        /// <para> Zwraca false, jeśli student nie oczekuje na akceptację prowadzącego. </para>
        /// </summary>
        public bool checkIfApplyingToProject(string projectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Zgłoszenia.Projekt")                  // Ładuje zgłoszenia studenta i powiązany z nimi projekt
                .Single();
            
            foreach (Zgłoszenie z in stud.Zgłoszenia)
            {
                if ( z.Projekt != null && z.Projekt.nazwa.Equals(projectName) )
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Sprawdza, czy student nie wysłał już zgłoszenia na podany przedmiot.
        /// <para> Zwraca false, jeśli student nie oczekuje na akceptację prowadzącego. </para>
        /// </summary>
        public bool checkIfApplyingToSubject(string subjectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Zgłoszenia.Przedmiot")                  // Ładuje zgłoszenia studenta i powiązany z nimi przedmiot
                .Single();

            foreach (Zgłoszenie z in stud.Zgłoszenia)
            {
                if (z.Przedmiot != null && z.Przedmiot.nazwa.Equals(subjectName))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Sprawdza, czy student jest zapisany na przedmiot, do którego należy projekt, na który się zapisuje
        /// <para> Zwraca false, jeśli nie jest. </para>
        /// </summary>
        public bool checkIfEnrolledToSuperiorSubject(string projectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Przedmioty")                          // Ładuje przedmioty, na które jest zapisany student
                .Single();

            Przedmiot x = ( from subj in context.Przedmioty
                                join proj in context.Projekty on subj.PrzedmiotID equals proj.PrzedmiotID
                            where proj.nazwa.Equals(projectName)
                            select subj
                          ).FirstOrDefault();

            foreach (Przedmiot p in stud.Przedmioty)
            {
                if (p.PrzedmiotID == x.PrzedmiotID)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Sprawdza, czy student jest już zapisany na przedmiot
        /// <para> Zwraca false, jeśli nie jest. </para>
        /// </summary>
        public bool checkIfEnrolledToSubject(string subjectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Przedmioty")                          // Ładuje przedmioty, na które jest zapisany student
                .Single();

            foreach (Przedmiot p in stud.Przedmioty)
            {
                if ( p.nazwa.Equals(subjectName) )
                    return true;
            }

            return false;
        }

        #endregion

        #region Usuwanie
        //----------------------------------------------------------------

        /// <summary>
        /// Usuwa studenta z przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveFromSubject(string subjectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Przedmioty").Include("Projekty").Include("Oceny").Include("Zgłoszenia")   // Załaduje wszystkie Navigation Properties
                .Single();
            
            Przedmiot subj = context.Przedmioty.Where(p => p.nazwa.Equals(subjectName)).Single();
            
            stud.Przedmioty.Remove(subj);
            subj.liczbaStudentów--;

            //--------------------------------
            // Usuwanie zależnych rekordów - projekty i oceny z danego przedmiotu, a także zgłoszenia na realizowane w nim projekty
            var projectsList = stud.Projekty.Where( p => p.Przedmiot.Equals(subj) ).ToList();
            projectsList.ForEach( proj => stud.Projekty.Remove(proj) );

            var gradesList = stud.Oceny.Where( o => o.Przedmiot.Equals(subj) ).ToList();
            context.Oceny.RemoveRange(gradesList);                  // Całkowicie usuwa oceny z bazy

            var applicationsList = stud.Zgłoszenia.Where( z => z.Przedmiot.Equals(subj) ).ToList();
            context.Zgłoszenia.RemoveRange( applicationsList );     // Całkowicie usuwa zgłoszenia z bazy

            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa studenta z projektu o podanej nazwie
        /// </summary>
        public void RemoveFromProject(string projectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Projekty").Include("Oceny")   // Załadowane zostaną również Navigation Properties z powiązanymi projektami i ocenami
                .Single();

            Projekt proj = context.Projekty.Where(p => p.nazwa.Equals(projectName)).Single();
            stud.Projekty.Remove(proj);

            var gradesList = stud.Oceny.Where( o => o.Projekt.Equals(proj) ).ToList();
            context.Oceny.RemoveRange(gradesList);      // Całkowicie usuwa oceny z bazy

            context.SaveChanges();
        }

        //----------------------------------------------------------------
        #endregion
    }
}


// Zrobiona przypadkowo, może potem się przyda

//        /// <summary>
//        /// Pobiera z bazy przedmioty, na które nie jest zapisany student
//        /// </summary>
//        public List<PrzedmiotDTO> getNotMySubjects()
//        {
//            var teacherQuery = context.Database.SqlQuery<PrzedmiotDTO>(@"
//                            SELECT p.nazwa, prow.login AS prowadzący
//                            FROM Przedmiot p
//                                JOIN Użytkownik prow ON prow.UżytkownikID = p.ProwadzącyID
//                            WHERE p.nazwa NOT IN
//                            (
//	                            SELECT p.nazwa
//	                            FROM Przedmiot p
//		                            JOIN Przedmioty_studenci ps ON ps.PrzedmiotID = p.PrzedmiotID
//	                            WHERE ps.StudentID = (
//		                            SELECT s.UżytkownikID
//		                            FROM Użytkownik s
//		                            WHERE s.login = '" + userName + @"')
//                            )");

//            return teacherQuery.ToList();
//        }