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
        /// Pobiera z bazy listę użytkowników, których login zawiera w sobie podane słowo
        /// </summary>
        public List<UżytkownikDTO> getUser(string loginFragment)
        {
            return studDatabase.getUser(loginFragment);
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
        public List<ForeignProjektDTO> getNotMyProjects(string subjectName)
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
        public List<StudentDTO> getStudentsFromProject(string projectName)
        {
            return studDatabase.getStudentsFromProject(projectName);
        }

        /// <summary>
        /// Pobiera oceny z podanego przedmiotu
        /// </summary>
        public List<OcenaDTO> getGradesFromSubject(string subjectName)
        {
            return studDatabase.getGradesFromSubject(subjectName);
        }

        /// <summary>
        /// Pobiera oceny z podanego projektu
        /// </summary>
        public List<OcenaZProjektuDTO> getGradesFromProject(string projectName)
        {
            return studDatabase.getGradesFromProject(projectName);
        }

        /// <summary>
        /// Zapisuje studenta na przedmiot o podanej nazwie.
        /// <para> Zwraca false, jeśli student jest już zapisany na dany przedmiot. </para>
        /// </summary>
        public bool enrollToSubject(string subjectName)
        {
            return studDatabase.enrollToSubject(subjectName);
        }

        /// <summary>
        /// Zapisuje studenta na projekt o podanej nazwie.
        /// </summary>
        public void enrollToProject(string projectName)
        {
            studDatabase.enrollToProject(projectName);
        }

        /// <summary>
        /// Usuwa studenta z przedmiotu o podanej nazwie
        /// </summary>
        public void RemoveFromSubject(string subjectName)
        {
            studDatabase.RemoveFromSubject(subjectName);
        }

        /// <summary>
        /// Usuwa studenta z projektu o podanej nazwie
        /// </summary>
        public void RemoveFromProject(string projectName)
        {
            studDatabase.RemoveFromProject(projectName);
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