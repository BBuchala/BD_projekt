using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Użytkownik
    {
        public int UżytkownikID { get; set; }               // Primary Key
        public int RozmowaID { get; set; }                  // Foreign Key
        // chyba do usunięcia, rozmów może być dużo

        [MaxLength(50)]
        [Required]
        public string login { get; set; }

        [MaxLength(50)]
        [Required]
        public string hasło { get; set; }

        [MaxLength(50)]
        [Required]
        public string email { get; set; }

        [MaxLength(100)]
        public string miejsceZamieszkania { get; set; }

        public DateTime? dataUrodzenia { get; set; }

        public virtual ICollection<Rozmowa> Rozmowy { get; set; }
    }
}
