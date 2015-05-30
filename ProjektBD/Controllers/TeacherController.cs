using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Databases;
using ProjektBD.Model;

namespace ProjektBD.Controllers
{
    /// <summary>
    /// Kontroler dla formularza prowadzącego
    /// </summary>
    class TeacherController : Controller
    {
        TeacherDatabase teacherdb;

        public TeacherController()
        {
            database = new TeacherDatabase();
            teacherdb = (database as TeacherDatabase);
        }


        public List<Zgłoszenie> getProjectApplications(string teacherLogin)
        {
            return teacherdb.getProjectApplications(teacherLogin);
        }

        public List<Zgłoszenie> getSubjectApplications(string teacherLogin)
        {
            return teacherdb.getSubjectApplications(teacherLogin);
        }
    }
}
