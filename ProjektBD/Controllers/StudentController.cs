using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjektBD.Databases;
using ProjektBD.Model;

namespace ProjektBD.Controllers
{
    class StudentController : UserController
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        StudentDatabase studDatabase;

        public StudentController(string userName)
        {
            database = usrDatabase = new StudentDatabase(userName);
            studDatabase = (usrDatabase as StudentDatabase);
        }

        #endregion

        #region Pobieranie
        //----------------------------------------------------------------
        
        #region Przedmioty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera przedmioty studenta z bazy
        /// </summary>
        public List<PrzedmiotDTO> getMySubjects()
        {
            return studDatabase.getMySubjects();
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z bazy projekty użytkownika realizowane w ramach przedmiotu
        /// </summary>
        public List<ProjektDTO> getMyProjects(string subjectName)
        {
            return studDatabase.getMyProjects(subjectName);
        }

        /// <summary>
        /// Pobiera z bazy projekty realizowane w ramach przedmiotu, na które nie jest zapisany student
        /// </summary>
        public List<ForeignProjektDTO> getNotMyProjects(string subjectName)
        {
            return studDatabase.getNotMyProjects(subjectName);
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Zapisywanie
        //----------------------------------------------------------------

        /// <summary>
        /// Zapisuje studenta na przedmiot o podanej nazwie.
        /// <para> Zwraca string określający stan operacji.</para>
        /// </summary>
        public string enrollToSubject(string subjectName)
        {
            if ( studDatabase.checkIfApplyingToSubject(subjectName) )
                return "Znaleziono aplikację na przedmiot";

            if ( studDatabase.checkIfEnrolledToSubject(subjectName) )
                return "Już zapisany";

            studDatabase.enrollToSubject(subjectName);

            return "Zapisywanie zakończone pomyślnie";
        }

        /// <summary>
        /// Zapisuje studenta na projekt o podanej nazwie.
        /// <para> Zwraca string określający stan operacji. </para>
        /// </summary>
        public string enrollToProject(string projectName)
        {
            if ( studDatabase.checkIfApplyingToProject(projectName) )
                return "Znaleziono aplikację na projekt";

            if ( !studDatabase.checkIfEnrolledToSuperiorSubject(projectName) )
                return "Niezapisany na przedmiot nadrzędny";

            studDatabase.enrollToProject(projectName);

            return "Zapisywanie zakończone pomyślnie";
        }

        //----------------------------------------------------------------
        #endregion
    }
}
