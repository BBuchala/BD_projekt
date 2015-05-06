using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.DAL;
using ProjektBD.Utilities;

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
        /// Wyświetla MessageBox z informacją o wyłączonej bazie
        /// </summary>
        public static void notifyAboutEmergencyMode()
        {
            MsgBoxUtils.displayInformationMsgBox("Prace konserwacyjne", "Baza danych jest obecnie wyłączona. Proszę spróbować później");
        }
    }
}