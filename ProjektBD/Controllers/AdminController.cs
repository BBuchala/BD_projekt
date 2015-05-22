using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// Pobiera nazwy zakładów istniejących w bazie
        /// </summary>
        public string[] getInstituteNames()
        {
            return admDatabase.getInstituteNames().ToArray();
        }

        /// <summary>
        /// Pobiera wszystkie wiersze z tabeli o podanej nazwie
        /// </summary>
        public IList getTableData(string tableName)
        {
            Type genericParameterType = Type.GetType("ProjektBD.Model." + tableName);

            MethodInfo method = typeof(AdminDatabase).GetMethod("getTableData");

            MethodInfo genericMethod = method.MakeGenericMethod(genericParameterType);      // Tworzy metodę generyczną o typie podanym przez parametr

            var result = genericMethod.Invoke(admDatabase, new object[] { tableName });

            return (result as IList);
        }

        /// <summary>
        /// Pobiera nazwy kluczy głównych z tabeli o podanej nazwie
        /// </summary>
        public List<string> getPrimaryKeyNames(string tableName)
        {
            Type modelClassType = Type.GetType("ProjektBD.Model." + tableName);         // Typ klasy encji
            Type baseClassType = modelClassType.BaseType;                               // Typ jej klasy bazowej

            MethodInfo method = typeof(AdminDatabase).GetMethod("getPrimaryKeyNames");
            MethodInfo genericMethod;

            if (baseClassType.Name.Equals("Object"))
                genericMethod = method.MakeGenericMethod(modelClassType);               // Jeśli klasa jest bazowa
            else
                genericMethod = method.MakeGenericMethod(baseClassType);                // Jeśli klasa jest pochodna

            var result = genericMethod.Invoke(admDatabase, null);

            return (result as List<string>);
        }

        /// <summary>
        /// Sprawdza, czy kontekst posiada nowe dane, które musi wysłać do bazy
        /// </summary>
        public bool doesContextHaveChanges()
        {
            return admDatabase.doesContextHaveChanges();
        }

        /// <summary>
        /// Pobiera prowadzących z bazy
        /// </summary>
        public List<ProwadzącyDTO> getTeachers()
        {
            return admDatabase.getTeachers();
        }
    }
}