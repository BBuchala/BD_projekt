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
        #region Field & Constructor

        /// <summary>
        /// Klasa z bazodanowymi metodami prowadzącego
        /// </summary>
        TeacherDatabase teacherdb;

        /// <summary>
        /// Konstruktor, zrzutowanie database na odpowiedni typ.
        /// </summary>
        public TeacherController()
        {
            database = new TeacherDatabase();
            teacherdb = (database as TeacherDatabase);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Pobieranie zgłoszeń na projekt dla danego prowadzącego.
        /// </summary>
        /// <param name="teacherLogin">Login sprawdzanego prowadzącego</param>
        /// <returns>Lista zgłoszeń dla danego prowadzącego</returns>
        public List<ZgłoszenieNaProjektDTO> getProjectApplications(string teacherLogin)
        {
            return teacherdb.getProjectApplications(teacherLogin);
        }

        /// <summary>
        /// Pobieranie zgłoszeń na przedmiot dla danego prowadzącego.
        /// </summary>
        /// <param name="teacherLogin">Login sprawdzanego prowadzącego</param>
        /// <returns>Lista zgłoszeń dla danego prowadzącego</returns>
        public List<ZgłoszenieNaPrzedmiotDTO> getSubjectApplications(string teacherLogin)
        {
            return teacherdb.getSubjectApplications(teacherLogin);
        }

        /// <summary>
        /// Dodawanie studenta do przedmiotu (pozytywne rozpatrzenie).
        /// </summary>
        /// <param name="applicationID">Akceptowane zgłoszenie.</param>
        public void addStudentToSubject(long applicationID)
        {
            teacherdb.addStudentToSubject(applicationID);
        }

        /// <summary>
        /// Dodawanie studenta do projektu (pozytywne rozpatrzenie).
        /// </summary>
        /// <param name="applicationID">Akceptowane zgłoszenie.</param>
        public void addStudentToProject(long applicationID)
        {
            teacherdb.addStudentToProject(applicationID);
        }

        /// <summary>
        /// Usunięcie zgłoszenia z bazy.
        /// </summary>
        /// <param name="applicationID">ID usuwanego zgłoszenia</param>
        public void deleteApplication(long applicationID)
        {
            teacherdb.deleteApplication(applicationID);
        }
        #endregion
    }
}
