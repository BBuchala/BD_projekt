using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Model
{
    //---------------------
    // ~Data Transfer Objects~
    // Klasy pomocnicze do obsługi tablic, których nie ma w kontekście
    // (do nich zwracane są wyniki zapytań)
    //---------------------
    class Prowadzone_rozmowy
    {
        public int RozmowaID { get; set; }
        public int UżytkownikID { get; set; }
    }

    class Przedmioty_studenci
    {
        public int PrzedmiotID { get; set; }
        public int StudentID { get; set; }
    }

    class ProwadzącyDTO
    {
        public string login { get; set; }
        public string email { get; set; }
        public string nazwaZakładu { get; set; }
    }
}