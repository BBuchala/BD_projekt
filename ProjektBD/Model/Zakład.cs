using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    class Zakład
    {
        public short ZakładID { get; set; }           // Primary Key

        public string nazwa { get; set; }
        public string opis { get; set; }

        public virtual ICollection<Prowadzący> Prowadzący { get; set; }
    }
}
