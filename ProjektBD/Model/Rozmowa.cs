using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Rozmowa
    {
        public Rozmowa()
        {
            Użytkownicy = new HashSet<Użytkownik>();
            Wiadomości = new HashSet<Wiadomość>();
        }

        public int RozmowaID { get; set; }          // Primary Key

        public DateTime dataRozpoczęcia { get; set; }

        [Browsable(false)]
        public virtual ICollection<Użytkownik> Użytkownicy { get; set; }

        [Browsable(false)]
        public virtual ICollection<Wiadomość> Wiadomości { get; set; }
    }
}
