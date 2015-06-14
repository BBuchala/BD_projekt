using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ProjektBD.Custom_Controls;
using ProjektBD.Databases;
using ProjektBD.Forms;
using ProjektBD.Model;

namespace ProjektBD.Controllers
{
    /// <summary>
    /// Kontroler dla formularza prowadzącego
    /// </summary>
    class TeacherController : UserController
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        /// <summary>
        /// Klasa z bazodanowymi metodami prowadzącego
        /// </summary>
        TeacherDatabase teacherdb;

        /// <summary>
        /// Konstruktor, zrzutowanie database na odpowiedni typ.
        /// </summary>
        public TeacherController(string teacherLogin)
        {
            database = usrDatabase = new TeacherDatabase(teacherLogin);
            teacherdb = (usrDatabase as TeacherDatabase);
        }

        //----------------------------------------------------------------
        #endregion

        #region Zgłoszenia
        //----------------------------------------------------------------

        /// <summary>
        /// Pobieranie zgłoszeń na projekt dla danego prowadzącego.
        /// </summary>
        /// <param name="teacherLogin">Login sprawdzanego prowadzącego</param>
        /// <returns>Lista zgłoszeń dla danego prowadzącego</returns>
        public List<ZgłoszenieNaProjektDTO> getProjectApplications(string teacherLogin)
        {
            return teacherdb.getProjectApplications(teacherLogin);
        }

        /// <summary>
        /// Pobieranie zgłoszeń na przedmiot dla danego prowadzącego.
        /// </summary>
        /// <param name="teacherLogin">Login sprawdzanego prowadzącego</param>
        /// <returns>Lista zgłoszeń dla danego prowadzącego</returns>
        public List<ZgłoszenieNaPrzedmiotDTO> getSubjectApplications(string teacherLogin)
        {
            return teacherdb.getSubjectApplications(teacherLogin);
        }

        /// <summary>
        /// Usunięcie zgłoszenia z bazy.
        /// </summary>
        /// <param name="applicationID">ID usuwanego zgłoszenia</param>
        public void deleteApplication(long applicationID)
        {
            teacherdb.deleteApplication(applicationID);
        }

        //----------------------------------------------------------------
        #endregion

        #region Dodawanie do przedmiotu/projektu
        //----------------------------------------------------------------

        /// <summary>
        /// Dodawanie studenta do przedmiotu (pozytywne rozpatrzenie).
        /// </summary>
        /// <param name="applicationID">Akceptowane zgłoszenie.</param>
        public void addStudentToSubject(long applicationID)
        {
            teacherdb.addStudentToSubject(applicationID);
        }

        /// <summary>
        /// Dodawanie studenta do projektu (pozytywne rozpatrzenie).
        /// </summary>
        /// <param name="applicationID">Akceptowane zgłoszenie.</param>
        public void addStudentToProject(long applicationID)
        {
            teacherdb.addStudentToProject(applicationID);
        }

        //----------------------------------------------------------------
        #endregion

        #region Pobieranie
        //----------------------------------------------------------------

        #region Przedmioty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera przedmioty prowadzącego z bazy
        /// </summary>
        /// <returns></returns>
        public List<PrzedmiotProwadzącegoDTO> getMySubjects()
        {
            return teacherdb.getMySubjects();
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z bazy projekty studenta z podanego przedmiotu
        /// </summary>
        public List<ForeignProjektDTO> getStudentProjects(string studentIndexNumber, string subjectName)
        {
            int indexNumber = Int32.Parse(studentIndexNumber);

            return teacherdb.getStudentProjects(indexNumber, subjectName);
        }

       

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Raport
        //----------------------------------------------------------------

        public string getSubjectInfo(string subjectName)
        {
            PrzedmiotRaportDetailsDTO newobject = teacherdb.getSubjectInfo(subjectName);

            string calosc = "Nazwa przedmiotu: " + newobject.nazwa + "\r\n" +
                            "Ilość projektów: " + newobject.liczbaProjektów + "\r\n" +
                            "Ilość studentów zapisanych na przedmiot: " + newobject.liczbaStudentów + "\r\n" +
                            "Nazwa prowadzącego: "+newobject.prowadzący + "\r\n";

            return calosc;

        }

        public string getZestawienieOcen(string subjectName)
        {
            string[] dane = new string [4];
            string var;

            List<StudentDTO> newoceny =  teacherdb.getZestawienieOcen(subjectName,"4","5");
            List<StudentDTO> newoceny1 = teacherdb.getZestawienieOcen(subjectName,"3.5","4");
            List<StudentDTO> newoceny2 = teacherdb.getZestawienieOcen(subjectName,"3","3.5");
            List<StudentDTO> newoceny3 = teacherdb.getZestawienieOcen(subjectName,"0","3");

            foreach (StudentDTO current in newoceny)
                dane[0] = current.login + ", " + dane[0];

            foreach (StudentDTO current in newoceny1)
                dane[1] = current.login + ", " + dane[1];

            foreach (StudentDTO current in newoceny2)
                dane[2] = current.login + ", " + dane[2];

            foreach (StudentDTO current in newoceny3)
                dane[3] = current.login + ", " + dane[3];
            
            for (int i = 0; i < 4; i++)
            {
                if (dane[i] == null)
                    dane[i] = "brak";
                else
                    dane[i] = dane[i].Remove(dane[i].Length - 2, 2);            // usuwa przecinek i spację na końcu linijki
            }

            var = "Lista użytkowników z średnią ocen od 4 do 5:  " + dane[0] + "\r\n" +
                  "Lista użytkowników z średnią ocen od 3,5 do 4:  " + dane[1] + "\r\n" +
                  "Lista użytkowników z średnią ocen od 3 do 3,5:  " + dane[2] + "\r\n" +
                  "Lista użytkowników z średnią ocen od 1 do 3 (Niezaliczony):  " + dane[3] + "\r\n";

            return var;
        }

        public string getZestawienieStudenciProjekty(string subjectName)
        {
            string[] tab = new string[1000];
            int i = 0;
            string calosc = "LISTA PROJEKTOW I ICH UCZESTNIKOW \r\n";
            string calosc1 = "" ;
            
            List<ProjektDTO> newprojekty = teacherdb.getProjekty(subjectName);
            List<StudentDTO>[] tab2 = new List<StudentDTO>[1000];

            foreach (ProjektDTO current in newprojekty)
            {
                tab[i] = current.nazwa;
                i++;
            }

            for (int j = 0; j < i; j++)
            {
                tab2[j] = teacherdb.getStudenciZProjektow(tab[j]);
                calosc += tab[j] + ":" + "\r\n";

                foreach ( StudentDTO current in tab2[j] )
                    calosc1 = current.login + ", " + calosc1;

                if (calosc1 == null)
                    calosc1 = "BRAK";
                else
                    calosc1 = calosc1.Remove(calosc1.Length - 2, 2);            // usuwa przecinek i spację na końcu linijki

                calosc = calosc + calosc1 + "\r\n";
                calosc1 = null;
            }

            return calosc;
        }

        public string getNdst(string subjectName)
        {
            string tmp = "OSOBY Z OCENAMI NIEDOSTATECZNYMI + ICH DANE: \r\n";
            List<StudentDTO> newoceny = teacherdb.getNiedostateczne(subjectName);

            foreach (StudentDTO current in newoceny)
            {
                tmp += current.login + " - " + current.email + ", ";
            }

            tmp = tmp.Remove(tmp.Length - 2, 2);            // usuwa przecinek i spację na końcu linijki

            return tmp;
        }

        //----------------------------------------------------------------
        #endregion

        #region Usuwanie
        //----------------------------------------------------------------

        /// <summary>
        /// Usuwa z bazy przedmiot o podanej nazwie
        /// </summary>
        public void removeSubject(string subjectName)
        {
            teacherdb.removeSubject(subjectName);
        }

        /// <summary>
        /// Usuwa z bazy projekt o podanej nazwie
        /// </summary>
        public void removeProject(string projectName)
        {
            teacherdb.removeProject(projectName);
        }

        /// <summary>
        /// Usuwa z bazy ocenę o podanym ID
        /// </summary>
        public void removeGrade(long gradeID)
        {
            teacherdb.removeGrade(gradeID);
        }

        /// <summary>
        /// Usuwa z bazy studenta o podanym numerze indeksu
        /// </summary>
        public void removeStudent(string subjectName, string projectName, string studentIndexNumber)
        {
            int indexNumber = Int32.Parse(studentIndexNumber);

            teacherdb.removeStudent(subjectName, projectName, indexNumber);
        }

        //----------------------------------------------------------------
        #endregion

        #region Dodawanie
        //----------------------------------------------------------------

        /// <summary>
        /// Dodaje studentowi ocenę z podanego przedmiotu lub projektu
        /// </summary>
        public void addGrade(string studentLogin, OcenaDetailsDTO grade)
        {
            if (grade.nazwaProjektu != null)
                teacherdb.addProjectGrade(studentLogin, grade);
            else
                teacherdb.addSubjectGrade(studentLogin, grade);
        }

        //----------------------------------------------------------------
        #endregion

        #region Modyfikowanie
        //----------------------------------------------------------------

        /// <summary>
        /// Modyfikuje podaną ocenę
        /// </summary>
        public void modifyGrade(long gradeID, double newValue, string newDesc)
        {
            teacherdb.modifyGrade(gradeID, newValue, newDesc);
        }

        //----------------------------------------------------------------
        #endregion

        #region Metody pomocnicze
        //----------------------------------------------------------------

        /// <summary>
        /// Pobiera z kontenera wszystkie kontrolki podanego typu.
        /// </summary>
        /// <typeparam name="T">Typ kontrolek, które chcemy pobrać</typeparam>
        /// <param name="container">Kontener, z którego pobieramy kontrolki</param>
        /// <returns>Lista szukanych kontrolek</returns>
        public List<T> GetAllControlsRecursive<T>(Control container) where T : Control
        {
            var controlsList = new List<T>();

            foreach (Control item in container.Controls)
            {
                var control = (item as T);                      // Sprawdza, czy pobrana kontrolka jest listView'em

                if (control != null)
                    controlsList.Add(control);                  // Jeśli tak, dodaje ją do wynikowej listy
                else
                    controlsList.AddRange( GetAllControlsRecursive<T>(item) );      // Jeśli nie, traktuje jako kolejny kontener i wchodzi w głąb rekursji
            }
            return controlsList;
        }

        //----------------------------------------------------------------
        #endregion

    }


}
