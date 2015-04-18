using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Exceptions
{
    /// <summary>
    /// Wyjątek rzucany wtedy, gdy mamy 2 lub więcej użytkowników o tym samym loginie lub mailu.
    /// Może być użyty np. w logowaniu, rejestracji.
    /// Istnieje możliwość uogólnienia na wszystkie typy rekordów (np. niemożliwe utworzenie 2 projektów o tej samej nazwie itp.)
    /// </summary>
    public class UsersOverlappingException: Exception
    {
        /// <summary>
        /// Nasz message. Message z dużej litery jest systemowe!
        /// </summary>
        private string message;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public UsersOverlappingException()
        {
        }

        /// <summary>
        /// Konstruktor z parametrem message.
        /// </summary>
        /// <param name="message">Wiadomość wyrzucana przy wyjątku.</param>
        public UsersOverlappingException(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Getter dla pola prywatnego message.
        /// </summary>
        /// <returns>Zwraca wiadomość wyrzucaną przy wyjątku.</returns>
        public string getMessage()
        {
            return this.message;
        }

    }
}
