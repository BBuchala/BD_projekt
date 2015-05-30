using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Model;

namespace ProjektBD.Databases
{
    /// <summary>
    /// Baza danych dla formularza prowadzącego.
    /// </summary>
    class TeacherDatabase : DatabaseBase
    {

        internal List<Zgłoszenie> getProjectApplications(string teacherLogin)
        {
            var query = context.Database.SqlQuery<Zgłoszenie>("SELECT * " +
                                                            "FROM ZGŁOSZENIE WHERE ProwadzącyID = " +  getTeacherID(teacherLogin) + 
                                                            " AND jestZaakceptowane = 'false' AND ProjektID IS NOT NULL").ToList();

            return query;
        }

        internal List<Zgłoszenie> getSubjectApplications(string teacherLogin)
        {
            var query = context.Database.SqlQuery<Zgłoszenie>("SELECT * " +
                                                            "FROM ZGŁOSZENIE WHERE ProwadzącyID = " + getTeacherID(teacherLogin) +
                                                            " AND jestZaakceptowane = 'false' AND ProjektID IS NULL").ToList();

            return query;

        }

        public int getTeacherID(string teacherLogin)
        {
            return context.Użytkownicy.Local.Where(s => s.login.Equals(teacherLogin)).FirstOrDefault().UżytkownikID;
        }
    }
}
