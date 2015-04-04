using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Wyjątek rzucany wtedy, gdy mamy 2 lub więcej użytkowników o tym samym loginie lub mailu.
 * Może być użyty np. w logowaniu, rejestracji.
 * Istnieje możliwość uogólnienia na wszystkie typy rekordów (np. niemożliwe utworzenie 2 projektów o tej samej nazwie itp.)
 */ 
namespace ProjektBD.Exceptions
{
    public class UsersOverlappingException: Exception
    {
        // Nasz message. Message z dużej litery jest systemowe!
        string message;

        public UsersOverlappingException()
        {
        }

        public UsersOverlappingException(string message)
        {
            this.message = message;
        }

        public string getMessage()
        {
            return this.message;
        }

    }
}
