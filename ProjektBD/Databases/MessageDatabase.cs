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
    }
}
