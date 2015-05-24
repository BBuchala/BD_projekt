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
    }
}