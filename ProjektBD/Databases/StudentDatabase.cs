using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektBD.Model;

namespace ProjektBD.Databases
{
    class StudentDatabase : DatabaseBase
    {
        //TODO: pobrać z bazy ID użytkownika w konstruktorze i przechować je jako prywatne pole
        //      zoptymalizować niektóre zapytania (parę zbędnych JOIN'ów)

        public StudentDatabase(string studentName)
        {
            this.userName = studentName;
        }

        /// <summary>
        /// Pobiera przedmioty z bazy
        /// </summary>
        public List<PrzedmiotDTO> getSubjects()
        {
            // Tworzy zapytanie tak przejebane, że w okienku Debuga się nie mieści,
            // za to nie generuje wyjątku-widmo

            //var teacherQuery = from p in context.Przedmioty
            //                   join sensei in context.Prowadzący on p.Prowadzący equals sensei
            //                   join o in context.PrzedmiotyObieralne on p.PrzedmiotID equals o.PrzedmiotID into ob

            //                   from obierka in ob.DefaultIfEmpty()
            //                   select new PrzedmiotDTO
            //                   {
            //                       nazwa = p.nazwa,
            //                       liczbaStudentów = p.liczbaStudentów,
            //                       maxLiczbaStudentów = obierka.maxLiczbaStudentów,
            //                       prowadzący = sensei.login
            //                   };

            var teacherQuery = context.Database.SqlQuery<PrzedmiotDTO>(@"
                            SELECT p.nazwa, prow.login AS prowadzący
                            FROM Przedmiot p
                                LEFT JOIN PrzedmiotObieralny ob ON ob.PrzedmiotID = p.PrzedmiotID
                                JOIN Użytkownik prow ON prow.UżytkownikID = p.ProwadzącyID");

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
	                            LEFT JOIN PrzedmiotObieralny ob ON ob.PrzedmiotID = p.PrzedmiotID
	                            JOIN Użytkownik prow ON prow.UżytkownikID = p.ProwadzącyID
	                            JOIN Przedmioty_studenci ps ON ps.PrzedmiotID = p.PrzedmiotID
                            WHERE ps.StudentID = (
	                            SELECT s.UżytkownikID
	                            FROM Użytkownik s
	                            WHERE s.login = '" + userName + "')");

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
                                ps.StudentID = ( SELECT u.UżytkownikID
                                                 FROM Użytkownik u
                                                 WHERE u.login = '" + userName + "')");

            return projectQuery.ToList();
        }

        /// <summary>
        /// Pobiera z bazy projekty realizowane w ramach przedmiotu, na które nie jest zapisany student
        /// </summary>
        public List<ProjektDTO> getNotMyProjects(string subjectName)
        {
            var projectQuery = context.Database.SqlQuery<ProjektDTO>(@"
                            SELECT p.nazwa, p.maxLiczbaStudentów
                            FROM Projekt p
                                JOIN Przedmiot subj ON subj.PrzedmiotID = p.PrzedmiotID
                            WHERE subj.nazwa = '" + subjectName + @"' AND
	                            p.nazwa NOT IN
	                            (
		                            SELECT p.nazwa
		                            FROM Projekt p
			                            JOIN Projekty_studenci ps ON ps.ProjektID = p.ProjektID
		                            WHERE ps.StudentID = (SELECT u.UżytkownikID
							                              FROM Użytkownik u
							                              WHERE u.login = '" + userName + @"')
                                )");

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
        public List<StudentDTO> getStudentsFromProject(string subjectName, string projectName)
        {
            var studentQuery = context.Database.SqlQuery<StudentDTO>(@"
                            SELECT s.nrIndeksu, u.login, u.email
                            FROM Student s
	                            JOIN Użytkownik u ON u.UżytkownikID = s.UżytkownikID
	                            JOIN Przedmioty_studenci ps ON ps.StudentID = s.UżytkownikID
	                            JOIN Przedmiot subj ON subj.PrzedmiotID = ps.PrzedmiotID
	                            JOIN Projekty_studenci projs ON projs.StudentID = s.UżytkownikID
	                            JOIN Projekt proj ON proj.ProjektID = projs.ProjektID
                            WHERE subj.nazwa = '" + subjectName + @"' AND
                                proj.nazwa = '" + projectName + "'");

            return studentQuery.ToList();
        }

        /// <summary>
        /// Pobiera oceny z podanego przedmiotu
        /// </summary>
        public List<OcenaDTO> getGradesFromSubject(string subjectName)
        {
            var gradeQuery = context.Database.SqlQuery<OcenaDTO>(@"
                            SELECT o.wartość, o.dataWpisania
                            FROM Ocena o
	                            JOIN Przedmiot p ON p.PrzedmiotID = o.PrzedmiotID
	                            JOIN Student s ON s.UżytkownikID = o.StudentID
	                            JOIN Użytkownik usr ON usr.UżytkownikID = s.UżytkownikID
                            WHERE usr.login = '" + userName + @"' AND
	                            p.nazwa = '" + subjectName + "'");

            return gradeQuery.ToList();
        }

        /// <summary>
        /// Pobiera oceny z podanego projektu
        /// </summary>
        public List<OcenaDTO> getGradesFromProject(string subjectName, string projectName)
        {
            var gradeQuery = context.Database.SqlQuery<OcenaDTO>(@"
                            SELECT o.wartość, o.dataWpisania
                            FROM Ocena o
	                            JOIN Przedmiot p ON p.PrzedmiotID = o.PrzedmiotID
	                            JOIN Student s ON s.UżytkownikID = o.StudentID
	                            JOIN Użytkownik usr ON usr.UżytkownikID = o.StudentID
	                            JOIN Projekty_studenci ps ON
		                            ps.StudentID = o.StudentID AND
		                            ps.ProjektID = o.ProjektID
	                            JOIN Projekt proj ON proj.ProjektID = o.ProjektID
                            WHERE p.nazwa = '" + subjectName + @"' AND
	                            usr.login = '" + userName + @"' AND
	                            proj.nazwa = '" + projectName + "'");

            return gradeQuery.ToList();
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