using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Model;

namespace ProjektBD.Databases
{
    class StudentDatabase : DatabaseBase
    {
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
                            SELECT p.nazwa, p.liczbaStudentów, ob.maxLiczbaStudentów, prow.login AS prowadzący
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
                            SELECT p.nazwa, p.liczbaStudentów, ob.maxLiczbaStudentów, prow.login AS prowadzący
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
    }
}