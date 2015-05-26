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
        public string userSalt(string login)
        {
            context.Użytkownicy.Load();

            return context.Użytkownicy.Local
                .Where(u => u.login.Equals(login))
                .Select(s => s.sól)
                .FirstOrDefault();
        }
        public void changeUserPersonalAccount(string login, TextBoxBase nemail, DateTime ndataUrodzenia, TextBoxBase nmiejscowosc)
        {
            context.Użytkownicy.Load();
            Użytkownik u = context.Użytkownicy.Local
                .Where(a => a.login.Equals(login))
                .FirstOrDefault();

            u.email = nemail.Text;
            u.dataUrodzenia = ndataUrodzenia;
            u.miejsceZamieszkania = nmiejscowosc.Text;

            context.SaveChanges();
        }

        public void changeUserPasswordAccount(string login, string shasło, string nhasło,string soln)
        {
            context.Użytkownicy.Load();
            Użytkownik u = context.Użytkownicy.Local
                .Where(s => s.login.Equals(login) && s.hasło.Equals(shasło))
                .FirstOrDefault();

            u.hasło = nhasło;
            u.sól = soln;
            context.SaveChanges();
         }

    }
}
