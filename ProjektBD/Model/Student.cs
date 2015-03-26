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
    [Table("Student")]
    class Student : Użytkownik
    {
        public Student()
        {
            Oceny = new HashSet<Ocena>();
            Przedmioty = new HashSet<Przedmiot>();
            Zgłoszenia = new HashSet<Zgłoszenie>();
        }

        [Index(IsUnique = true)]                    // Sprawia, że atrybut będzie unikalny
        [Required]
        public int nrIndeksu { get; set; }

        [Browsable(false)]                          // Dzięki temu nie navigation property nie pojawi się w dataGridView
        public virtual ICollection<Ocena> Oceny { get; set; }

        [Browsable(false)]
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }

        [Browsable(false)]
        public virtual ICollection<Zgłoszenie> Zgłoszenia { get; set; }
    }
}
