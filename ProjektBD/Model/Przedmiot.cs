using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Przedmiot
    {
        public int PrzedmiotID { get; set; }            // Primary Key

        [ForeignKey("Prowadzący")]
        public int ProwadzącyID { get; set; }           // Foreign Key

        public string nazwa { get; set; }
        public string opis { get; set; }
        public int liczbaStudentów { get; set; }

        public virtual Prowadzący Prowadzący { get; set; }
        public virtual ICollection<Student> Studenci { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Projekt> Projekty { get; set; }
        public virtual ICollection<Raport> Raporty { get; set; }
        public virtual ICollection<Zgłoszenie> Zgłoszenia { get; set; }
    }
}
