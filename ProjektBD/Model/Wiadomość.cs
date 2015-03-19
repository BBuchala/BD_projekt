using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Wiadomość
    {
        public int WiadomośćID { get; set; }            // Primary Key
        public int RozmowaID { get; set; }              // Foreign Key

        public DateTime dataWysłania { get; set; }
        public string nadawca { get; set; }
        public string treść { get; set; }

        public virtual Rozmowa Rozmowa { get; set; }  
    }
}
