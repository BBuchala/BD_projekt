using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    [Table("Prowadzący")]
    class Prowadzący : Użytkownik
    {
        public short ZakładID { get; set; }             // Foreign Key
        public string nazwaZakładu { get; set; }

        public virtual Zakład Zakład { get; set; }
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        public virtual ICollection<Raport> Raporty { get; set; }
        public virtual ICollection<Zgłoszenie> Zgłoszenia { get; set; }
    }
}
