using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using ProjektBD.Model;

namespace ProjektBD.Databases
{
    abstract class UserDatabase : DatabaseBase
    {
        /// <summary>
        /// ID aktualnego użytkownika
        /// </summary>
        protected int userID;

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
                                user.login.Equals("admin") == false
                            select new UżytkownikDTO
                            {
                                login = user.login,
                                stanowisko = (user is Student) ? "Student" : "Prowadzący"
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
            var teacherQuery = from subj in context.Przedmioty
                               join prow in context.Prowadzący on subj.ProwadzącyID equals prow.UżytkownikID
                               select new PrzedmiotDTO { nazwa = subj.nazwa, prowadzący = prow.login };

            return teacherQuery.ToList();
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera projekty realizowane w ramach przedmiotu
        /// </summary>
        public List<ProjektDTO> getProjects(string subjectName)
        {
            var projectQuery = from proj in context.Projekty
                               join subj in context.Przedmioty on proj.PrzedmiotID equals subj.PrzedmiotID
                               where subj.nazwa.Equals(subjectName)
                               select new ProjektDTO { nazwa = proj.nazwa, maxLiczbaStudentów = proj.maxLiczbaStudentów };

            return projectQuery.ToList();
        }

        //----------------------------------------------------------------
        #endregion

        #region Oceny
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera oceny studenta z podanego przedmiotu
        /// </summary>
        public List<OcenaDTO> getGradesFromSubject(string studentLogin, string subjectName)
        {
            var gradeQuery = from o in context.Oceny
                                join subj in context.Przedmioty on o.PrzedmiotID equals subj.PrzedmiotID
                                join stud in context.Użytkownicy on o.StudentID equals stud.UżytkownikID
                             where stud.login.Equals(studentLogin) &&
                                 subj.nazwa.Equals(subjectName)
                             select new OcenaDTO
                             {
                                 nazwaProjektu = o.Projekt.nazwa,
                                 wartość = o.wartość,
                                 dataWpisania = o.dataWpisania,
                                 ocenaID = o.OcenaID
                             };

            return gradeQuery.ToList();
        }

        /// <summary>
        /// Pobiera oceny studenta z podanego projektu
        /// </summary>
        public List<OcenaZProjektuDTO> getGradesFromProject(string studentLogin, string projectName)
        {
            var gradeQuery = from o in context.Oceny
                                join proj in context.Projekty on o.ProjektID equals proj.ProjektID
                                join user in context.Studenci on o.StudentID equals user.UżytkownikID
                             where user.login.Equals(studentLogin) &&
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

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Usuwanie
        //----------------------------------------------------------------

        /// <summary>
        /// Usuwa podanego studenta z przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveFromSubject(string studentLogin, string subjectName)
        {
            Student stud = context.Studenci
                .Where( s => s.login.Equals(studentLogin) )
                .Include("Przedmioty").Include("Projekty").Include("Oceny").Include("Zgłoszenia")   // Załaduje wszystkie Navigation Properties
                .Single();

            Przedmiot subj = context.Przedmioty.Where( p => p.nazwa.Equals(subjectName) ).Single();

            stud.Przedmioty.Remove(subj);
            subj.liczbaStudentów--;

            //--------------------------------
            // Usuwanie zależnych rekordów - projekty i oceny z danego przedmiotu, a także zgłoszenia na realizowane w nim projekty
            var projectsList = stud.Projekty.Where( p => p.Przedmiot.Equals(subj) ).ToList();
            projectsList.ForEach(proj => stud.Projekty.Remove(proj));

            var gradesList = stud.Oceny.Where( o => o.Przedmiot.Equals(subj) ).ToList();
            context.Oceny.RemoveRange(gradesList);                  // Całkowicie usuwa oceny z bazy

            var applicationsList = stud.Zgłoszenia.Where( z => z.Przedmiot.Equals(subj) ).ToList();
            context.Zgłoszenia.RemoveRange(applicationsList);     // Całkowicie usuwa zgłoszenia z bazy

            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa podanego studenta z projektu o podanej nazwie
        /// </summary>
        public void RemoveFromProject(string studentLogin, string projectName)
        {
            Student stud = context.Studenci
                .Where( s => s.login.Equals(studentLogin) )
                .Include("Projekty").Include("Oceny")   // Załadowane zostaną również Navigation Properties z powiązanymi projektami i ocenami
                .Single();

            Projekt proj = context.Projekty.Where( p => p.nazwa.Equals(projectName) ).Single();
            stud.Projekty.Remove(proj);

            var gradesList = stud.Oceny.Where( o => o.Projekt.Equals(proj) ).ToList();
            context.Oceny.RemoveRange(gradesList);      // Całkowicie usuwa oceny z bazy

            context.SaveChanges();
        }

        //----------------------------------------------------------------
        #endregion
    }
}
