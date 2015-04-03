using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * Wyjątek do używania w przypadku nie wypełnienia pól tekstowych.
 */ 
namespace ProjektBD.Exceptions
{
    class EmptyFieldException: Exception
    {
        /*
         * Jak mamy parę pól tekstowych, a chcemy zwrócić informację o które pole
         * nam chodzi, to do tego słuzy ten indeks.
         */ 
        private int fieldNumber;

        public EmptyFieldException()
        {
        }

        public EmptyFieldException(string message)
        {

        }

        public EmptyFieldException(int number)
        {
            this.fieldNumber = number;
        }

        public int getFieldNumber()
        {
            return this.fieldNumber;
        }

    }
}
