using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProjektBD.Exceptions
{
    /// <summary>
    /// Wyjątek do używania w przypadku nie wypełnienia pól tekstowych.
    /// </summary>
    class EmptyFieldException: Exception
    {
        /// <summary>
        /// Jak mamy parę pól tekstowych, a chcemy zwrócić informację o które pole nam chodzi, to do tego słuzy ten indeks.
        /// </summary>
        private int fieldNumber;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public EmptyFieldException()
        {
        }

        /// <summary>
        /// Konstruktor z parametrem.
        /// </summary>
        /// <param name="message">Wiadomość przekazana podczas wyrzucania wyjątku.</param>
        public EmptyFieldException(string message)
        {

        }

        /// <summary>
        /// Konstruktor z paramterem (int).
        /// </summary>
        /// <param name="number">Indeks nieprawidłowego pola.</param>
        public EmptyFieldException(int number)
        {
            this.fieldNumber = number;
        }

        /// <summary>
        /// Getter dla pola prywatnego fieldNumber.
        /// </summary>
        /// <returns>Zwraca indeks nieprawidłowego pola.</returns>
        public int getFieldNumber()
        {
            return this.fieldNumber;
        }

    }
}
