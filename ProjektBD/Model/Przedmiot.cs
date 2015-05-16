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
    class Przedmiot
    {
        public Przedmiot()
        {
            Studenci = new HashSet<Student>();          // By dało się dodać nowego studenta do przedmiotu
            Oceny = new HashSet<Ocena>();
            Projekty = new HashSet<Projekt>();
            Raporty = new HashSet<Raport>();
            Zgłoszenia = new HashSet<Zgłoszenie>();
        }

        public int PrzedmiotID { get; set; }            // Primary Key

        [ForeignKey("Prowadzący")]
        public int ProwadzącyID { get; set; }           // Foreign Key

        [MaxLength(50)]
        [Required]
        public string nazwa { get; set; }

        [MaxLength(1000)]
        public string opis { get; set; }

        public int liczbaStudentów { get; set; }

        [Browsable(false)]
        public virtual Prowadzący Prowadzący { get; set; }

        [Browsable(false)]
        public virtual ICollection<Student> Studenci { get; set; }

        [Browsable(false)]
        public virtual ICollection<Ocena> Oceny { get; set; }

        [Browsable(false)]
        public virtual ICollection<Projekt> Projekty { get; set; }

        [Browsable(false)]
        public virtual ICollection<Raport> Raporty { get; set; }

        [Browsable(false)]
        public virtual ICollection<Zgłoszenie> Zgłoszenia { get; set; }
    }
}
