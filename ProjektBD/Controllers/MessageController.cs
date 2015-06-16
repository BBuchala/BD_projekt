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

        /// <summary>
        /// Pobiera z bazy wiadomości z podanej rozmowy
        /// </summary>
        public List<Wiadomość> getMessages(int conversationID)
        {
            return msgDatabase.getMessages(conversationID);
        }

        /// <summary>
        /// Pobiera z bazy nieprzeczytane wiadomości z podanej rozmowy
        /// </summary>
        public List<Wiadomość> getNewMessages(int conversationID, string userLogin)
        {
            return msgDatabase.getNewMessages(conversationID, userLogin);
        }

        /// <summary>
        /// Wysyła wiadomość o podanej treści.
        /// </summary>
        /// <param name="userLogin"> Login użytkownika wysyłającego wiadomość </param>
        /// <param name="sendDate"> Data wysłania wiadomości </param>
        /// <param name="msgContents"> Treść wiadomości </param>
        /// <param name="conversationID"> ID rozmowy </param>
        public void sendMessage(string userLogin, DateTime sendDate, string msgContents, int conversationID)
        {
            msgDatabase.sendMessage(userLogin, sendDate, msgContents, conversationID);
        }

        /// <summary>
        /// Szuka nowych, nieprzeczytanych przez użytkownika wiadomości
        /// </summary>
        public List<int> findConversationsWithNewMessages(string userLogin)
        {
            return msgDatabase.findConversationsWithNewMessages(userLogin);
        }

        /// <summary>
        /// Zaznacza wybraną konwersację jako przeczytaną
        /// </summary>
        public void setConversationAsReaded(int conversationID, string userLogin)
        {
            msgDatabase.setConversationAsReaded(conversationID, userLogin);

        }
    }
}
