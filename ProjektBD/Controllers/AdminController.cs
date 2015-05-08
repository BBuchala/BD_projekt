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
    /// Kontroler dla formularza administratora
    /// Myślę, że przyda się, gdy zaczniemy bindować tabele
    /// </summary>
    class AdminController : Controller
    {
        AdminDatabase admDatabase;

        public AdminController()
        {
            database = new AdminDatabase();
            admDatabase = (database as AdminDatabase);
        }

        /// <summary>
        /// Wyszukuje i zwraca listę użytkowników starających się o uprawnienia prowadzącego
        /// </summary>
        public List<Użytkownik> findNewUsers()
        {
            return admDatabase.findUsers();
        }

        /// <summary>
        /// Dodaje do bazy nowego prowadzącego
        /// </summary>
        public void addTeacher(Użytkownik u)
        {
            admDatabase.addTeacher(u);
        }

        /// <summary>
        /// Usuwa podanego użytkownika z bazy
        /// </summary>
        public void deleteUser(Użytkownik u)
        {
            admDatabase.deleteUser(u);
        }
    }
}