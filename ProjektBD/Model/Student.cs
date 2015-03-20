using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    [Table("Student")]
    class Student : Użytkownik
    {
        [Index(IsUnique = true)]                    // Sprawia, że atrybut będzie unikalny
        [Required]
        public int nrIndeksu { get; set; }

        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        public virtual ICollection<Zgłoszenie> Zgłoszenia { get; set; }
    }
}
