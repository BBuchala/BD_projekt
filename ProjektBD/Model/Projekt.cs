using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Projekt
    {
        public Projekt()
        {
            Oceny = new HashSet<Ocena>();
        }

        public int ProjektID { get; set; }          // Primary Key
        public int PrzedmiotID { get; set; }        // Foreign Key

        [MaxLength(50)]
        [Required]
        public string nazwa { get; set; }

        [MaxLength(1000)]
        public string opis { get; set; }

        public int maxLiczbaStudentów { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }

        [Browsable(false)]
        public virtual ICollection<Ocena> Oceny { get; set; }
    }
}
