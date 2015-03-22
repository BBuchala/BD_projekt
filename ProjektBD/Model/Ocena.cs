using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Ocena
    {
        public long OcenaID { get; set; }           // Primary Key
        public int PrzedmiotID { get; set; }        // Foreign Key

        [ForeignKey("Student")]
        public int StudentID { get; set; }          // Foreign Key

        [Required]
        public double wartość { get; set; }

        [MaxLength(500)]
        public string komentarz { get; set; }

        public DateTime? dataWpisania { get; set; }

        public virtual Student Student { get; set; }
        public virtual Przedmiot Przedmiot { get; set; }
    }
}
