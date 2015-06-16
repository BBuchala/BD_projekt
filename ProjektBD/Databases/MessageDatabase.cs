using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using ProjektBD.Model;

namespace ProjektBD.Databases
{
    class MessageDatabase : DatabaseBase
    {
        int userID;

        public MessageDatabase(string userLogin)
        {
            userID = context.Użytkownicy
                .Where(u => u.login.Equals(userLogin))
                .Select(i => i.UżytkownikID)
                .Single();
        }

        /// <summary>
        /// Pobiera z bazy kontakty użytkownika
        /// </summary>
        public List<KontaktyDTO> getContacts()
        {
            var result = context.Database.SqlQuery<KontaktyDTO>(@"
                                SELECT pr.RozmowaID, u.login AS rozmówcy
                                FROM Prowadzone_rozmowy pr
	                                JOIN Użytkownik u ON pr.UżytkownikID = u.UżytkownikID
                                WHERE pr.RozmowaID in	(
							                                SELECT pr.RozmowaID
							                                FROM Prowadzone_rozmowy pr
							                                WHERE pr.UżytkownikID = " + userID + @"
						                                )
	                                AND pr.UżytkownikID != " + userID + @"
                                ORDER BY pr.rozmowaID, u.login");

            return result.ToList();
        }

        /// <summary>
        /// Wyszukuje w bazie użytkownika o podanym loginie
        /// </summary>
        public Użytkownik findUser(string login)
        {
            return context.Użytkownicy
                .Where(x => x.login.Equals(login))
                .Single();
        }

        /// <summary>
        /// Pobiera z bazy listę użytkowników, których login zawiera w sobie podane słowo.
        /// <para> W wyszukiwaniu pomija aktualnego uzytkownika. </para>
        /// </summary>
        public List<UżytkownikDTO> getUsers(string loginFragment)
        {
            var query = from user in context.Użytkownicy
                        where user.login.Contains(loginFragment) &&
                            user.login.Equals("admin") == false &&
                            user.UżytkownikID != userID
                        select new UżytkownikDTO
                        {
                            login = user.login,
                            stanowisko = (user is Student) ? "Student" : "Prowadzący"
                        };

            return query.ToList();
        }

        /// <summary>
        /// Tworzy nową rozmowę między użytkownikami podanymi przez parametr
        /// </summary>
        public void addConversation(List<Użytkownik> usersList)
        {
            Rozmowa r = new Rozmowa { dataRozpoczęcia = DateTime.Now };
            context.Rozmowy.Add(r);

            usersList.ForEach(u => u.Rozmowy.Add(r));
            context.SaveChanges();
        }

        /// <summary>
        /// Usuwa z bazy rozmowę o podanym ID
        /// </summary>
        public void deleteConversation(int conversationID)
        {
            Rozmowa conversation = context.Rozmowy
                .Where( c => c.RozmowaID == conversationID )
                .Single();

            context.Rozmowy.Remove(conversation);
            context.SaveChanges();
        }

        /// <summary>
        /// Pobiera z bazy wiadomości z podanej rozmowy
        /// </summary>
        public List<Wiadomość> getMessages(int conversationID)
        {
            return context.Wiadomości
                .Where( w => w.RozmowaID == conversationID )
                .ToList();
        }

        /// <summary>
        /// Pobiera z bazy nieprzeczytane wiadomości z podanej rozmowy
        /// </summary>
        public List<Wiadomość> getNewMessages(int conversationID, string userLogin)
        {
            return context.Wiadomości
                .Where(w => w.RozmowaID == conversationID &&
                    w.nadawca != userLogin &&
                    w.przeczytana == false)
                .ToList();
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
            Wiadomość msg = new Wiadomość
            {
                nadawca = userLogin,
                dataWysłania = sendDate,
                treść = msgContents,
                przeczytana = false,
                RozmowaID = conversationID
            };

            context.Wiadomości.Add(msg);
            context.SaveChanges();
        }

        /// <summary>
        /// Szuka rozmów z nowymi, nieprzeczytanymi przez użytkownika wiadomościami
        /// </summary>
        /// <returns> ID rozmów, w których znajdują się nieprzeczytane wiadomości </returns>
        public List<int> findConversationsWithNewMessages(string userLogin)
        {
            var query = context.Database.SqlQuery<int>(@"
                        SELECT DISTINCT msg.RozmowaID
                        FROM Wiadomość msg
	                        JOIN Prowadzone_rozmowy pr ON msg.RozmowaID = pr.RozmowaID
                        WHERE pr.UżytkownikID = " + userID + @" AND
	                        msg.nadawca != '" + userLogin + @"' AND
	                        msg.przeczytana = 0");

            return query.ToList();
        }

        /// <summary>
        /// Zaznacza wybraną konwersację jako przeczytaną
        /// </summary>
        public void setConversationAsReaded(int conversationID, string userLogin)
        {
            // Pobranie ID nieprzeczytanych wiadomości
            var query = context.Database.SqlQuery<int>(@"
                        SELECT msg.WiadomośćID
                        FROM Wiadomość msg
	                        JOIN Prowadzone_rozmowy pr ON msg.RozmowaID = pr.RozmowaID
                        WHERE pr.UżytkownikID = " + userID + @" AND
                            pr.RozmowaID = " + conversationID + @" AND
	                        msg.nadawca != '" + userLogin + @"' AND
	                        msg.przeczytana = 0");

            List<int> msgIDList = query.ToList();

            List<Wiadomość> msgList = new List<Wiadomość>(msgIDList.Count);             // Właściwa lista wiadomości

            // Pobranie prawdziwych, śledzonych przez kontekst wiadomości na podstawie ich pobranych wcześniej ID
            foreach (int msgID in msgIDList)
                msgList.Add( context.Wiadomości.Single(w => w.WiadomośćID == msgID) );

            msgList.ForEach( m => m.przeczytana = true );
            context.SaveChanges();
        }
    }
}
