using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjektBD.Databases;
using ProjektBD.Model;

namespace ProjektBD.Controllers
{
    abstract class UserController : Controller
    {
        protected UserDatabase usrDatabase;

        #region Pobieranie
        //----------------------------------------------------------------

        #region Użytkownicy
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z bazy listę użytkowników, których login zawiera w sobie podane słowo
        /// </summary>
        public List<UżytkownikDTO> getUser(string loginFragment)
        {
            return usrDatabase.getUser(loginFragment);
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
            return usrDatabase.getSubjects();
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera projekty z bazy
        /// </summary>
        public List<ProjektDTO> getProjects(string subjectName)
        {
            return usrDatabase.getProjects(subjectName);
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
            return usrDatabase.getStudentsFromSubject(subjectName);
        }

        /// <summary>
        /// Pobiera studentów zapisanych na projekt z danego przedmiotu
        /// </summary>
        public List<StudentDTO> getStudentsFromProject(string projectName)
        {
            return usrDatabase.getStudentsFromProject(projectName);
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
            return usrDatabase.getGradesFromSubject(studentLogin, subjectName);
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
            usrDatabase.RemoveFromSubject(studentLogin, subjectName);
        }

        /// <summary>
        /// Usuwa podanego studenta z projektu o podanej nazwie
        /// </summary>
        public void RemoveFromProject(string studentLogin, string projectName)
        {
            usrDatabase.RemoveFromProject(studentLogin, projectName);
        }

        //----------------------------------------------------------------
        #endregion
    }
}
