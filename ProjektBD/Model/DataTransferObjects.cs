using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektBD.Utilities;

namespace ProjektBD.Model
{
    //---------------------
    // Klasy pomocnicze, do których wracane są wyniki zapytań
    //---------------------

    #region Złączenia M:N
    //----------------------------------------------------------------

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

    class Projekty_studenci
    {
        public int ProjektID { get; set; }
        public int StudentID { get; set; }
    }

    //----------------------------------------------------------------
    #endregion

    #region ListView'y
    //----------------------------------------------------------------

    class ProwadzącyDTO
    {
        public string login { get; set; }
        public string email { get; set; }
        public string nazwaZakładu { get; set; }
    }

    class PrzedmiotDTO          // Obierki wyróżnione innym kolorem?
    {
        public string nazwa { get; set; }
        public string prowadzący { get; set; }
    }

    class PrzedmiotProwadzącegoDTO
    {
        public string nazwa { get; set; }
        public int liczbaStudentów { get; set; }
        public int maxLiczbaStudentów { get; set; }
    }

    class ProjektDTO
    {
        public string nazwa { get; set; }
        public int maxLiczbaStudentów { get; set; }
    }

    class ForeignProjektDTO
    {
        public string nazwa { get; set; }
        public int liczbaStudentów { get; set; }
        public int maxLiczbaStudentów { get; set; }
    }

    class StudentDTO
    {
        public int nrIndeksu { get; set; }
        public string login { get; set; }
        public string email { get; set; }
    }

    class UżytkownikDTO
    {
        public string login { get; set; }
        public string stanowisko { get; set; }
    }

    class OcenaDTO
    {
        public string nazwaProjektu { get; set; }
        public double wartość { get; set; }
        public DateTime? dataWpisania { get; set; }

        [SkipInListView]
        public long ocenaID { get; set; }
    }

    class OcenaZProjektuDTO
    {
        public double wartość { get; set; }
        public DateTime? dataWpisania { get; set; }

        [SkipInListView]
        public long ocenaID { get; set; }
    }

    //----------------------------------------------------------------
    #endregion

    #region Szczegóły (przedmiotu, projektu, oceny)
    //----------------------------------------------------------------
    
    public class PrzedmiotDetailsDTO
    {
        public string nazwa { get; set; }
        public int liczbaStudentów { get; set; }
        public int? maxLiczbaStudentów { get; set; }
        public string prowadzący { get; set; }
        public string opis { get; set; }
    }

    public class ProjektDetailsDTO
    {
        public string nazwa { get; set; }
        public string nazwaPrzedmiotu { get; set; }
        public int liczbaStudentów { get; set; }
        public int maxLiczbaStudentów { get; set; }
        public string opis { get; set; }
    }

    public class OcenaDetailsDTO
    {
        public string nazwaPrzedmiotu { get; set; }
        public string nazwaProjektu { get; set; }
        public double wartość { get; set; }
        public DateTime? dataWpisania { get; set; }
        public string komentarz { get; set; }
    }

    //----------------------------------------------------------------
    #endregion

    #region Zgłoszenia
    //----------------------------------------------------------------

    class ZgłoszenieNaProjektDTO
    {
        public string loginStudenta { get; set; }
        public string nazwaProjektu { get; set; }
        public int numerIndeksu { get; set; }
        public string nazwaPrzedmiotu { get; set; }
        public long IDZgłoszenia { get; set; }
    }

    class ZgłoszenieNaPrzedmiotDTO
    {
        public string loginStudenta { get; set; }
        public string nazwaPrzedmiotu { get; set; }
        public int numerIndeksu { get; set; }
        public long IDZgłoszenia { get; set; }
    }

    //----------------------------------------------------------------
    #endregion

    #region Profile użytkowników
    //----------------------------------------------------------------

    public class StudentProfileDTO
    {
        public string login { get; set; }
        public int nrIndeksu { get; set; }
        public string email { get; set; }
        public string miejsceZamieszkania { get; set; }
        public DateTime? dataUrodzenia { get; set; }
    }

    public class TeacherProfileDTO
    {
        public string login { get; set; }
        public string email { get; set; }
        public string miejsceZamieszkania { get; set; }
        public DateTime? dataUrodzenia { get; set; }
        public string nazwaZakładu { get; set; }
    }

    //----------------------------------------------------------------
    #endregion
}