using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Zakład
    {
        public short ZakładID { get; set; }           // Primary Key

        [MaxLength(50)]
        [Required]
        public string nazwa { get; set; }

        [MaxLength(1000)]
        public string opis { get; set; }

        public virtual ICollection<Prowadzący> Prowadzący { get; set; }
    }
}
