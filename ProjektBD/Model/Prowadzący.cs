using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    [Table("Prowadzący")]
    class Prowadzący : Użytkownik
    {
        public Prowadzący()
        {
            Przedmioty = new HashSet<Przedmiot>();
            Raporty = new HashSet<Raport>();
            Zgłoszenia = new HashSet<Zgłoszenie>();
        }

        public short? ZakładID { get; set; }             // Foreign Key

        [Browsable(false)]
        public virtual Zakład Zakład { get; set; }

        [Browsable(false)]
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }

        [Browsable(false)]
        public virtual ICollection<Raport> Raporty { get; set; }

        [Browsable(false)]
        public virtual ICollection<Zgłoszenie> Zgłoszenia { get; set; }
    }
}
