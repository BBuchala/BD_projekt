using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjektBD.Databases;
using ProjektBD.Model;

namespace ProjektBD.Controllers
{
    /// <summary>
    /// Kontroler dla formularza prowadzącego
    /// </summary>
    class TeacherController : UserController
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        /// <summary>
        /// Klasa z bazodanowymi metodami prowadzącego
        /// </summary>
        TeacherDatabase teacherdb;

        /// <summary>
        /// Konstruktor, zrzutowanie database na odpowiedni typ.
        /// </summary>
        public TeacherController(string teacherLogin)
        {
            database = usrDatabase = new TeacherDatabase(teacherLogin);
            teacherdb = (usrDatabase as TeacherDatabase);
        }

        //----------------------------------------------------------------
        #endregion

        #region Zgłoszenia
        //----------------------------------------------------------------

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
        /// Usunięcie zgłoszenia z bazy.
        /// </summary>
        /// <param name="applicationID">ID usuwanego zgłoszenia</param>
        public void deleteApplication(long applicationID)
        {
            teacherdb.deleteApplication(applicationID);
        }

        //----------------------------------------------------------------
        #endregion

        #region Dodawanie do przedmiotu/projektu
        //----------------------------------------------------------------

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

        //----------------------------------------------------------------
        #endregion

        #region Pobieranie
        //----------------------------------------------------------------

        #region Przedmioty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera przedmioty prowadzącego z bazy
        /// </summary>
        /// <returns></returns>
        public List<PrzedmiotProwadzącegoDTO> getMySubjects()
        {
            return teacherdb.getMySubjects();
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z bazy projekty studenta z podanego przedmiotu
        /// </summary>
        public List<ForeignProjektDTO> getStudentProjects(string studentIndexNumber, string subjectName)
        {
            int indexNumber = Int32.Parse(studentIndexNumber);
            return teacherdb.getStudentProjects(indexNumber, subjectName);
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion
    }
}
