using ProjektBD.Forms.HelpForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektBD.Utilities
{
    /// <summary>
    /// Utils do wyświetlania odpowiedniej formatki z pomocą.
    /// </summary>
    public static class HelpFormStrategy
    {
        /// <summary>
        /// Wyświetlana formatka.
        /// </summary>
        static Form helpForm;

        /// <summary>
        /// Stworzenie odpowiedniej formatki na podstawie parametru, wyświetlenie jej i pozbycie się jej.
        /// </summary>
        /// <param name="type">Typ wyświetlanej formatki pomocy.</param>
        public static void chooseHelpFormStrategy(HelpFormTypes type)
        {
            switch(type)
            {
                case (HelpFormTypes.Login):
                    helpForm = new LoginHelp();
                    break;

                case (HelpFormTypes.About):
                    helpForm = new AboutBox();
                    break;

                case (HelpFormTypes.Register):
                    helpForm = new RegisterHelp();
                    break;

                default:
                    break;
            }

            helpForm.ShowDialog();
            helpForm.Dispose();
        }
    }

    /// <summary>
    /// Zawiera typy formatek z podpowiedziami, by ograniczyć możliwość tworzenia tylko do formatek pomocy.
    /// </summary>
    public enum HelpFormTypes
    {
        Login,
        Register,
        Student,
        Teacher,
        Admin,
        Managment,
        Communicator,
        About
    };
}
