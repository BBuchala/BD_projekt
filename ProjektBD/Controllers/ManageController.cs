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
        TextBoxBase starehasło, nowehasło, email, miejscowosc, powtorzonehaslo;
        string login;
        DateTimePicker dataUrodzenia;
        ManageDatabase mandatabase;

        public ManageController(string loginfromform, TextBoxBase hasłostarefromform, TextBoxBase hasłonowefromform, TextBoxBase emailfromform, TextBoxBase miejscowoscfromform, DateTimePicker dataUrodzeniafromform, TextBoxBase powtorznehaslofromform)
        {
            login = loginfromform;
            starehasło = hasłostarefromform;
            nowehasło = hasłonowefromform;
            powtorzonehaslo = powtorznehaslofromform;
            email = emailfromform;
            miejscowosc = miejscowoscfromform;
            dataUrodzenia = dataUrodzeniafromform;

            database = new ManageDatabase();
            mandatabase = (database as ManageDatabase);
        }


        public string validateInput1()
        {
            if ( !checkLength1() )
                return "Zla dlugosc";

            if ( !SpellCheckUtilities.isValidEmail(email.Text) )
                return "Zla forma email";

            else
            {
                 mandatabase.changeUserPersonalAccount(login, email, dataUrodzenia.Value, miejscowosc);
                 return "ok";
            }
        }
        bool checkPasswords()
        {
            // Jeżeli hasła są różne
            if (powtorzonehaslo.Text != nowehasło.Text)
                return false;
            else
                return true;
        }

        public string validateInput2()
        {
            if (!checkLength2())
                return "Zla dlugosc";

            if (!checkPasswords())
                return "Rozne hasla";

            else
            {
                string sols = mandatabase.userSalt(login);
                string soln = Encryption.generateSalt();

                if (sols != null)
                {
                    string hashedsPassword = Encryption.HashPassword(starehasło.Text, sols);
                    string hashednPassword = Encryption.HashPassword(nowehasło.Text, soln);

                    bool czystare = mandatabase.changeUserPasswordAccount(login, hashedsPassword, hashednPassword, soln);

                    if (czystare == false)
                        return "Zle stare haslo";
                }

                return "ok";
            }
        }

        public void deleteUser()
        {
            mandatabase.deleteFromDatabase(login);
        }

        public bool checkLength1()
        {
            if ( email.Text.Equals("") || email.Text.Length < 3 || email.Text == null )
                return false;

            if ( miejscowosc.Text.Equals("") || miejscowosc.Text.Length < 3 || miejscowosc.Text == null )
                return false;

            return true;
        }

        public bool checkLength2()
        {
            if  (starehasło.Text.Equals("") || starehasło.Text.Length < 3 || starehasło.Text == null )
                return false;

            if ( nowehasło.Text.Equals("") || nowehasło.Text.Length < 3 || nowehasło.Text == null )
                return false;

            if ( powtorzonehaslo.Text.Equals("") || powtorzonehaslo.Text.Length < 3 || powtorzonehaslo.Text == null )
                return false;

            return true;
        }
    }
}
