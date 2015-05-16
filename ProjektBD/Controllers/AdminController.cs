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

        /// <summary>
        /// Pobiera nazwy tabel istniejących w bazie
        /// </summary>
        public string[] getTableNames()
        {
            List<string> tableList = admDatabase.getTableNames();
            tableList.RemoveAt(0);                                  // Usuwa systemową kolumnę MigrationHistory

            return tableList.ToArray();                             // Wpisanie tablicy do comboboxa to kwestia 1 linijki
        }

        /// <summary>
        /// Pobiera wszystkie wiersze z tablicy o podanej nazwie
        /// </summary>
        public async Task< List<object> > getTableData(string tableName)
        {
            return await admDatabase.getTableData(tableName);
        }
    }
}