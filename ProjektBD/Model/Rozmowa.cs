using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Rozmowa
    {
        public int RozmowaID { get; set; }          // Primary Key

        public DateTime dataRozpoczęcia { get; set; }

        public virtual ICollection<Użytkownik> Użytkownicy { get; set; }
        public virtual ICollection<Wiadomość> Wiadomości { get; set; }
    }
}
