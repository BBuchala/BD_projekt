using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Użytkownik
    {
        public int UżytkownikID { get; set; }               // Primary Key
        public int RozmowaID { get; set; }                  // Foreign Key

        public string login { get; set; }
        public string hasło { get; set; }
        public string email { get; set; }
        public DateTime dataUrodzenia { get; set; }
        public string miejsceZamieszkania { get; set; }

        public virtual ICollection<Rozmowa> Rozmowy { get; set; }
    }
}
