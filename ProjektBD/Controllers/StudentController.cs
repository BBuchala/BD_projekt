using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Databases;
using ProjektBD.Model;

namespace ProjektBD.Controllers
{
    class StudentController : Controller
    {
        StudentDatabase studDatabase;

        public StudentController(string userName)
        {
            database = new StudentDatabase(userName);
            studDatabase = (database as StudentDatabase);
        }

        /// <summary>
        /// Pobiera przedmioty z bazy
        /// </summary>
        public List<PrzedmiotDTO> getSubjects()
        {
            return studDatabase.getSubjects();
        }

        /// <summary>
        /// Pobiera przedmioty studenta z bazy
        /// </summary>
        public List<PrzedmiotDTO> getMySubjects()
        {
            return studDatabase.getMySubjects();
        }

        /// <summary>
        /// Pobiera projekty z bazy
        /// </summary>
        public List<ProjektDTO> getProjects(string subjectName)
        {
            return studDatabase.getProjects(subjectName);
        }

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
        public List<ProjektDTO> getNotMyProjects(string subjectName)
        {
            return studDatabase.getNotMyProjects(subjectName);
        }

        /// <summary>
        /// Pobiera studentów zapisanych na przedmiot
        /// </summary>
        public List<StudentDTO> getStudentsFromSubject(string subjectName)
        {
            return studDatabase.getStudentsFromSubject(subjectName);
        }

        /// <summary>
        /// Pobiera studentów zapisanych na projekt z danego przedmiotu
        /// </summary>
        public List<StudentDTO> getStudentsFromProject(string subjectName, string projectName)
        {
            return studDatabase.getStudentsFromProject(subjectName, projectName);
        }
    }
}




//      /// <summary>
//      /// Pobiera z bazy przedmioty, na które nie jest zapisany student
//      /// </summary>
//      public List<PrzedmiotDTO> getNotMySubjects()
//      {
//          return studDatabase.getNotMySubjects();
//      }