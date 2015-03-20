using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Raport
    {
        public short RaportID { get; set; }           // Primary Key
        public int PrzedmiotID { get; set; }        // Foreign Key

        [ForeignKey("Prowadzący")]
        public int ProwadzącyID { get; set; }           // Foreign Key

        [MaxLength(2000)]
        [Required]
        public string treść { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        public virtual Prowadzący Prowadzący { get; set; }
    }
}
