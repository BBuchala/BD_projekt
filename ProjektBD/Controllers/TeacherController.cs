using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjektBD.Databases;
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

        public string getSubjectInfo(string subjectName)
        {
            PrzedmiotRaportDetailsDTO newobject = teacherdb.getSubjectInfo(subjectName);
            string calosc = "";
            calosc = "Nazwa przedmiotu: "+ newobject.nazwa+"\r\nIlość projektów: "+newobject.liczbaProjektów +
                        "\r\nIlość studentów zapisanych na przedmiot: "+ newobject.liczbaStudentów+
                        "\r\nNazwa prowadzącego: "+newobject.prowadzący+"\r\n ";
                      

            return calosc;

        }
        public string getZestawienieOcen(string subjectName)
        {
            string[] dane = new string [4];
            string var;
            List<StudentDTO> newoceny =  teacherdb.getZestawienieOcen(subjectName,"4","7");
            List<StudentDTO> newoceny1 = teacherdb.getZestawienieOcen(subjectName,"3.5","4");
            List<StudentDTO> newoceny2 = teacherdb.getZestawienieOcen(subjectName,"3","3.5");
            List<StudentDTO> newoceny3 = teacherdb.getZestawienieOcen(subjectName,"0","3");
            foreach (StudentDTO current in newoceny)
            {
                dane[0] = current.login + ", " + dane[0];

            }
            foreach (StudentDTO current in newoceny1)
            {
                dane[1] = current.login + ", " + dane[1];

            }
            foreach (StudentDTO current in newoceny2)
            {
                dane[2] = current.login + ", " + dane[2];

            }
            foreach (StudentDTO current in newoceny3)
            {
                dane[3] = current.login + ", " + dane[3];

            }
            for (int i = 0; i < 4; i++)
            {
                if (dane[i] == null)
                {
                    dane[i] = "brak";
                }


            }
            var = "Lista użytkowników z średnią ocen od 4 do 6:  " + dane[0] + "\r\n" +
                  "Lista użytkowników z średnią ocen od 3,5 do 4:  " + dane[1] + "\r\n" +
                  "Lista użytkowników z średnią ocen od 3 do 3,5:  " + dane[2] + "\r\n" +
                  "Lista użytkowników z średnią ocen od 1 do 3 (Niezaliczony):  " + dane[3] + "\r\n";
            return var;
        }


        public string getZestawienieStudenciProjekty(string subjectName)
        {
            string[] tab = new string[100];
            int i = 0;
            string calosc = "LISTA PROJEKTOW I ICH UCZESTNIKOW \r\n";
            string calosc1 = "" ;
            
            List<ProjektDTO> newprojekty = teacherdb.getProjekty(subjectName);
            List<StudentDTO>[] tab2 = new List<StudentDTO>[100];
             foreach (ProjektDTO current in newprojekty)
            {
               tab[i] = current.nazwa;
              i++;
                
            }
             for (int j = 0; j< i; j++)
             {
                 tab2[j] = teacherdb.getStudenciZProjektow(tab[j]);
                 calosc = calosc + tab[j]+":" + "\r\n";
                 foreach (StudentDTO current in tab2[j])
                 {
                     calosc1 =current.login+", "+calosc1  ;

                 }
                 if (calosc1 == null)
                     calosc1 = "BRAK";
                 calosc = calosc + calosc1 + "\r\n";
                 calosc1 = null;
                 
             }

            return calosc;
        }
    }


}
