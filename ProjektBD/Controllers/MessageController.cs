using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Databases;
using ProjektBD.Model;

namespace ProjektBD.Controllers
{
    class MessageController : Controller
    {
        MessageDatabase msgDatabase;

        public MessageController(string login)
        {
            database = new MessageDatabase(login);

            msgDatabase = (database as MessageDatabase);
        }

        /// <summary>
        /// Pobiera z bazy kontakty użytkownika
        /// </summary>
        public List<KontaktyDTO> getContacts()
        {
            List<KontaktyDTO> contactsList = msgDatabase.getContacts();

            for (int i = 1; i < contactsList.Count; i++)
            {
                if (contactsList[i - 1].RozmowaID == contactsList[i].RozmowaID)
                {
                    contactsList[i - 1].rozmówcy += ", " + contactsList[i].rozmówcy;
                    //contactsList[i - 1].miejsceZamieszkania += ", " + contactsList[i].miejsceZamieszkania;

                    contactsList.RemoveAt(i);
                }
            }

            for (int i = 0; i < contactsList.Count; i++)
                contactsList[i].ID = i;

            return contactsList;
        }

        /// <summary>
        /// Wyszukuje w bazie użytkownika o podanym loginie
        /// </summary>
        public Użytkownik findUser(string login)
        {
            return msgDatabase.findUser(login);
        }

        /// <summary>
        /// Pobiera z bazy listę użytkowników, których login zawiera w sobie podane słowo.
        /// <para> W wyszukiwaniu pomija aktualnego uzytkownika. </para>
        /// </summary>
        public List<UżytkownikDTO> getUsers(string loginFragment)
        {
            return msgDatabase.getUsers(loginFragment);
        }

        /// <summary>
        /// Tworzy nową rozmowę między użytkownikami podanymi przez parametr
        /// </summary>
        public void addConversation(List<Użytkownik> usersList)
        {
            msgDatabase.addConversation(usersList);
        }

        /// <summary>
        /// Usuwa z bazy rozmowę o podanym ID
        /// </summary>
        public void deleteConversation(int conversationID)
        {
            msgDatabase.deleteConversation(conversationID);
        }
    }
}
