using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjektBD.Databases;
using ProjektBD.Exceptions;

namespace ProjektBD.Controllers
{

    /// <summary>
    /// Kontroler dla formularza zarządzania
    /// </summary>
    class ManageController : Controller
    {
        TextBoxBase hasło,email, miejscowosc;
        string login;
        DateTimePicker dataUrodzenia;
        ManageDatabase mandatabase;


        public ManageController(string loginfromform, TextBoxBase hasłofromform, TextBoxBase emailfromform, TextBoxBase miejscowoscfromform, DateTimePicker dataUrodzeniafromform)
        {
            login = loginfromform;
            hasło = hasłofromform;
            email = emailfromform;
            miejscowosc = miejscowoscfromform;
            dataUrodzenia = dataUrodzeniafromform;

            database = new ManageDatabase();
            mandatabase = (database as ManageDatabase);
        }
           public bool validateInput1(){

               if (!(SpellCheckUtilities.isValidEmail(email.Text)))
                   return false;
               else
               {
                   return true;
                   mandatabase.changeUserPersonalAccount(login, email, dataUrodzenia.Value, miejscowosc);
               }


           }

            
        
            
           
        

        }

       



    
}
