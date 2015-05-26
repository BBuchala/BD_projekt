using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjektBD.Databases;
using ProjektBD.Exceptions;
using ProjektBD.Utilities;

namespace ProjektBD.Controllers
{

    /// <summary>
    /// Kontroler dla formularza zarządzania
    /// </summary>
    class ManageController : Controller
    {
        TextBoxBase starehasło, nowehasło, email, miejscowosc;
        string login;
        DateTimePicker dataUrodzenia;
        ManageDatabase mandatabase;


        public ManageController(string loginfromform, TextBoxBase hasłostarefromform, TextBoxBase hasłonowefromform, TextBoxBase emailfromform, TextBoxBase miejscowoscfromform, DateTimePicker dataUrodzeniafromform)
        {
            login = loginfromform;
            starehasło = hasłostarefromform;
            nowehasło = hasłonowefromform;
            email = emailfromform;
            miejscowosc = miejscowoscfromform;
            dataUrodzenia = dataUrodzeniafromform;

            database = new ManageDatabase();
            mandatabase = (database as ManageDatabase);
        }
        public bool validateInput1()
        {

            if (!(SpellCheckUtilities.isValidEmail(email.Text)))
                return false;
            else
            {

                mandatabase.changeUserPersonalAccount(login, email, dataUrodzenia.Value, miejscowosc);
                return true;
            }


        }
        public bool validateInput2()
        {

            string sols = mandatabase.userSalt(login);
            string soln = Encryption.generateSalt();
            if (sols != null)
            {
                string hashedsPassword = Encryption.HashPassword(starehasło.Text, sols);
                string hashednPassword = Encryption.HashPassword(nowehasło.Text, soln);
                mandatabase.changeUserPasswordAccount(login, hashedsPassword, hashednPassword,soln);

            }
            return true;



        }





    } 
}
