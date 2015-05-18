using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Databases
{
    class ManageDatabase : DatabaseBase
    {
        public void changeUserPersonalAccount(string login, TextBoxBase nemail, DateTime ndataUrodzenia, TextBoxBase nmiejscowosc)
        {

            Użytkownik u = context.Użytkownicy.Local
                .Where(a => a.login.Equals(login))
                .FirstOrDefault();

            u.email = nemail.Text;
            u.dataUrodzenia = ndataUrodzenia;
            u.miejsceZamieszkania = nmiejscowosc.Text;
        }

         public void changeUserPasswordAccount(string login, string nhasło)
        {

            Użytkownik u = context.Użytkownicy.Local
                .Where(a => a.login.Equals(login))
                .FirstOrDefault();

            u.hasło = nhasło;
         }

    }
}
