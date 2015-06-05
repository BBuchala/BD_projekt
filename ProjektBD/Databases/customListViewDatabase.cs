using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektBD.Model;

namespace ProjektBD.Databases
{
    class customListViewDatabase : DatabaseBase
    {
        /// <summary>
        /// Pobiera informacje o profilu studenta z bazy
        /// </summary>
        public StudentProfileDTO getStudentProfileData(int nrIndeksu)
        {
            var query = from s in context.Studenci
                        where s.nrIndeksu == nrIndeksu
                        select new StudentProfileDTO
                        {
                            login = s.login,
                            nrIndeksu = s.nrIndeksu,
                            email = s.email,
                            miejsceZamieszkania = s.miejsceZamieszkania,
                            dataUrodzenia = s.dataUrodzenia
                        };

            return query.Single();
        }

        /// <summary>
        /// Pobiera informacje o profilu studenta z bazy
        /// </summary>
        public StudentProfileDTO getStudentProfileData(string studentLogin)
        {
            var query = from s in context.Studenci
                        where s.login.Equals(studentLogin)
                        select new StudentProfileDTO
                        {
                            login = s.login,
                            nrIndeksu = s.nrIndeksu,
                            email = s.email,
                            miejsceZamieszkania = s.miejsceZamieszkania,
                            dataUrodzenia = s.dataUrodzenia
                        };

            return query.Single();
        }

        /// <summary>
        /// Pobiera z bazy informacje o profilu prowadzącego dany przedmiot
        /// </summary>
        public TeacherProfileDTO getTeacherProfileFromSubject(string subjectName)
        {
            var query = from prow in context.Prowadzący
                            join subj in context.Przedmioty on prow.UżytkownikID equals subj.ProwadzącyID
                        where subj.nazwa.Equals(subjectName)
                        select new TeacherProfileDTO
                        {
                            login = prow.login,
                            email = prow.email,
                            miejsceZamieszkania = prow.miejsceZamieszkania,
                            dataUrodzenia = prow.dataUrodzenia,
                            nazwaZakładu = prow.Zakład.nazwa                // Sprawdź, czy Ci działa, Ervi ;p
                        };

            return query.Single();
        }

        /// <summary>
        /// Pobiera z bazy informacje o profilu prowadzącego
        /// </summary>
        public TeacherProfileDTO getTeacherProfile(string teacherLogin)
        {
            var query = from prow in context.Prowadzący
                        where prow.login.Equals(teacherLogin)
                        select new TeacherProfileDTO
                        {
                            login = prow.login,
                            email = prow.email,
                            miejsceZamieszkania = prow.miejsceZamieszkania,
                            dataUrodzenia = prow.dataUrodzenia,
                            nazwaZakładu = prow.Zakład.nazwa                // Tu też
                        };

            return query.Single();
        }
    }
}
