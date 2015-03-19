using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    [Table("PrzedmiotObieralny")]
    class PrzedmiotObieralny : Przedmiot
    {
        public int maxLiczbaStudentów { get; set; }
    }
}
