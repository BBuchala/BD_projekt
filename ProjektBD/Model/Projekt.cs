using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Projekt
    {
        public int ProjektID { get; set; }          // Primary Key
        public int PrzedmiotID { get; set; }        // Foreign Key

        public string nazwa { get; set; }
        public string opis { get; set; }
        public int maxLiczbaStudentów { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
    }
}
