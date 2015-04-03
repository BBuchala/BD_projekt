using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjektBD.Forms;

namespace ProjektBD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            /*Odpalamy Form1 a nie LoginForm jako pierwsze, żeby było głównym oknem aplikacji
            
            Application.Run(new Form1());
            //Application.Run(new LoginForm());*/

            //Inne podejście do sprawy: LoginForm jako formatka główna

            // Application.Run(new LoginForm());
            Application.Run(new RegisterForm());

        }
    }
}
