using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.DAL
{
    // Przy każdym uruchomieniu usuwa i tworzy bazę danych na nowo
    class ProjektBDInitializer : System.Data.Entity.DropCreateDatabaseAlways<ProjektBDContext>
    {
        protected override void Seed(ProjektBDContext context)
        {
            // TODO: wypełnić bazę danymi testowymi
        }
    }
}
