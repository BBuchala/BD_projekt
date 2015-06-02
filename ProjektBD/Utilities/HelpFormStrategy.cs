using ProjektBD.Forms.HelpForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektBD.Utilities
{
    public static class HelpFormStrategy
    {
        static Form helpForm;

        public static void chooseHelpFormStrategy(HelpFormTypes type)
        {
            switch(type)
            {
                case (HelpFormTypes.Login):
                    helpForm = new LoginHelp();
                    break;

                default:
                    break;
            }

            helpForm.ShowDialog();
            helpForm.Dispose();
        }
    }

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
