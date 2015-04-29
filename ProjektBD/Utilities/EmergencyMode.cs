using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.DAL;

namespace ProjektBD
{
    /// <summary>
    /// Klasa przechowująca informacje o stanie naprawczym
    /// </summary>
    static class EmergencyMode
    {
        /// <summary>
        /// Aktualny stan bazy danych
        /// </summary>
        public static bool isEmergency;

        /// <summary>
        /// Sprawdza, czy baza jest w stanie naprawczym
        /// </summary>
        public static void checkEmergencyMode()
        {
            using ( ProjektBDContext context = new ProjektBDContext() )
            {
                string databaseState = context.Database.SqlQuery<string>
                    (@" SELECT state_desc
                        FROM sys.databases
                        WHERE name = 'ProjektBD'").FirstOrDefault();

                if (databaseState.Equals("EMERGENCY"))
                    isEmergency = true;
                else
                    isEmergency = false;
            }
        }

        /// <summary>
        /// Wyświetla MessageBox z informacją o wyłączonej bazie
        /// </summary>
        public static void notifyAboutEmergencyMode()
        {
            MessageBox.Show("Baza danych jest obecnie wyłączona. Proszę spróbować później", "Prace konserwacyjne",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
