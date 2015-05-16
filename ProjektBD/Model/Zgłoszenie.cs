using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Zgłoszenie
    {
        public Zgłoszenie()
        {
            jestZaakceptowane = false;                  // Inicjalizowanie wartością domyślną
        }

        public long ZgłoszenieID { get; set; }          // Primary Key
        public int PrzedmiotID { get; set; }            // Foreign Key

        [ForeignKey("Student")]
        public int StudentID { get; set; }              // Foreign Key
        [ForeignKey("Prowadzący")]
        public int ProwadzącyID { get; set; }           // Foreign Key

        public bool jestZaakceptowane { get; set; }

        [Browsable(false)]
        public virtual Prowadzący Prowadzący { get; set; }

        [Browsable(false)]
        public virtual Student Student { get; set; }

        [Browsable(false)]
        public virtual Przedmiot Przedmiot { get; set; }
    }
}
