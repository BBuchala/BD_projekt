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
        // TODO:
        // - zamiast joinować, w LINQ wykorzystać navigation properties

        private int userID;

        public StudentDatabase(string studentName)
        {
            userID = context.Użytkownicy
                .Where( u => u.login.Equals(studentName) )
                .Select( u => u.UżytkownikID )
                .Single();
        }

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
                                dataWpisania = o.dataWpisania
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
                             select new OcenaZProjektuDTO { wartość = o.wartość, dataWpisania = o.dataWpisania }; 

            return gradeQuery.ToList();
        }

        /// <summary>
        /// Zapisuje studenta na przedmiot o podanej nazwie.
        /// <para> Zwraca false, jeśli student jest już zapisany na dany przedmiot. </para>
        /// </summary>
        public bool enrollToSubject(string subjectName)
        {
            Student stud = context.Studenci
                .Where( s => s.UżytkownikID == userID )
                .Include("Przedmioty")                  // Załadowany zostanie równiez Navigation Property z powiązanymi przedmiotami
                .Single();

            Przedmiot subj = context.Przedmioty.Where( p => p.nazwa.Equals(subjectName) ).Single();

            if ( stud.Przedmioty.Contains(subj) )
                return false;

            Zgłoszenie z = new Zgłoszenie
            {
                StudentID = stud.UżytkownikID,
                PrzedmiotID = subj.PrzedmiotID,
                ProwadzącyID = subj.ProwadzącyID,
                jestZaakceptowane = false
            };

            context.Zgłoszenia.Add(z);
            context.SaveChanges();

            return true;
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

        /// <summary>
        /// Usuwa studenta z przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveFromSubject(string subjectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Przedmioty")                  // Załadowany zostanie równiez Navigation Property z powiązanymi przedmiotami
                .Include("Projekty")
                .Single();

            Przedmiot subj = context.Przedmioty.Where(p => p.nazwa.Equals(subjectName)).Single();

            stud.Przedmioty.Remove(subj);
            subj.liczbaStudentów--;

            var projectsList = stud.Projekty.Where( p => p.Przedmiot.Equals(subj) ).ToList();
            projectsList.ForEach( proj => stud.Projekty.Remove(proj) );

            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa studenta z projektu o podanej nazwie
        /// </summary>
        public void RemoveFromProject(string projectName)
        {
            Student stud = context.Studenci
                .Where(s => s.UżytkownikID == userID)
                .Include("Projekty")                  // Załadowany zostanie równiez Navigation Property z powiązanymi projektami
                .Single();

            Projekt proj = context.Projekty.Where(p => p.nazwa.Equals(projectName)).Single();

            stud.Projekty.Remove(proj);
            context.SaveChanges();
        }
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