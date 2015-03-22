using System;
using System.Collections.Generic;
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
        public short ZakładID { get; set; }             // Foreign Key

        [MaxLength(50)]
        //[Required]
        public string nazwaZakładu { get; set; }        // wywalić?

        public virtual Zakład Zakład { get; set; }
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        public virtual ICollection<Raport> Raporty { get; set; }
        public virtual ICollection<Zgłoszenie> Zgłoszenia { get; set; }
    }
}
