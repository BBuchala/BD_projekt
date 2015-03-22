using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Wiadomość
    {
        public int WiadomośćID { get; set; }            // Primary Key
        public int RozmowaID { get; set; }              // Foreign Key

        [MaxLength(50)]
        [Required]
        public string nadawca { get; set; }             // zamienić na klucz obcy NadawcaID?

        [MaxLength(2000)]
        [Required]
        public string treść { get; set; }

        public DateTime dataWysłania { get; set; }

        public virtual Rozmowa Rozmowa { get; set; }  
    }
}
