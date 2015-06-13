using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ProjektBD.Custom_Controls;
using ProjektBD.Databases;
using ProjektBD.Forms;
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

        #region Usuwanie
        //----------------------------------------------------------------

        /// <summary>
        /// Usuwa z bazy przedmiot o podanej nazwie
        /// </summary>
        public void removeSubject(string subjectName)
        {
            teacherdb.removeSubject(subjectName);
        }

        /// <summary>
        /// Usuwa z bazy projekt o podanej nazwie
        /// </summary>
        public void removeProject(string projectName)
        {
            teacherdb.removeProject(projectName);
        }

        /// <summary>
        /// Usuwa z bazy ocenę o podanym ID
        /// </summary>
        public void removeGrade(long gradeID)
        {
            teacherdb.removeGrade(gradeID);
        }

        /// <summary>
        /// Usuwa z bazy studenta o podanym numerze indeksu
        /// </summary>
        public void removeStudent(string subjectName, string projectName, string studentIndexNumber)
        {
            int indexNumber = Int32.Parse(studentIndexNumber);

            teacherdb.removeStudent(subjectName, projectName, indexNumber);
        }

        //----------------------------------------------------------------
        #endregion

        #region Dodawanie
        //----------------------------------------------------------------

        /// <summary>
        /// Dodaje studentowi ocenę z podanego przedmiotu lub projektu
        /// </summary>
        public void addGrade(string studentLogin, OcenaDetailsDTO grade)
        {
            if (grade.nazwaProjektu != null)
                teacherdb.addProjectGrade(studentLogin, grade);
            else
                teacherdb.addSubjectGrade(studentLogin, grade);
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
            teacherdb.modifyGrade(gradeID, newValue, newDesc);
        }

        //----------------------------------------------------------------
        #endregion

        #region Metody pomocnicze
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z kontenera wszystkie kontrolki podanego typu.
        /// </summary>
        /// <typeparam name="T">Typ kontrolek, które chcemy pobrać</typeparam>
        /// <param name="container">Kontener, z którego pobieramy kontrolki</param>
        /// <returns>Lista szukanych kontrolek</returns>
        public List<T> GetAllControlsRecursive<T>(Control container) where T : Control
        {
            var controlsList = new List<T>();

            foreach (Control item in container.Controls)
            {
                var control = (item as T);                      // Sprawdza, czy pobrana kontrolka jest listView'em

                if (control != null)
                    controlsList.Add(control);                  // Jeśli tak, dodaje ją do wynikowej listy
                else
                    controlsList.AddRange( GetAllControlsRecursive<T>(item) );      // Jeśli nie, traktuje jako kolejny kontener i wchodzi w głąb rekursji
            }
            return controlsList;
        }

        //----------------------------------------------------------------
        #endregion
    }
}
