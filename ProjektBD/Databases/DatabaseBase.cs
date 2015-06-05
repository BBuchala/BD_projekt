using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjektBD.DAL;

namespace ProjektBD.Databases
{
    /// <summary>
    /// Abstrakcyjna klasa zawierająca pola i netody wspólne dla wszystkich baz
    /// </summary>
    abstract class DatabaseBase
    {
        #region Pola
        //----------------------------------------------------------------

        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        protected ProjektBDContext context = new ProjektBDContext();

        /// <summary>
        /// Określa, czy udało się połączyć z bazą danych
        /// </summary>
        public bool connectionSuccessful = false;

        /// <summary>
        /// ID aktualnego użytkownika
        /// </summary>
        protected int userID;

        //----------------------------------------------------------------
        #endregion

        /// <summary>
        /// Łączy się z bazą danych i dokonuje jej rekonstrukcji, jeśli modele nie są zgodne.
        /// Zwraca true, jeśli nastąpił błąd podczas połączenia i chcemy zakończyć działanie aplikacji.
        /// </summary>
        public bool connectToDB()
        {
            bool shouldCloseForm = false;

            try
            {
                context.Database.Initialize(false);
                context.Użytkownicy.Load();                 // Wczytuje do lokalnej kolekcji wszystkich użytkowników (w tym studentów, prowadzących itp.)

                connectionSuccessful = true;
            }

            catch (System.Data.SqlClient.SqlException)
            {
                DialogResult connRetry = MessageBox.Show("Nastąpił błąd podczas próby połączenia z bazą danych.\n Upewnij się, czy nie jesteś połączony w innym miejscu. \n Spróbować ponownie?",
                                                       "Błąd połączenia",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (connRetry == DialogResult.No)
                    shouldCloseForm = true;
                else
                    connectToDB();
            }

            return shouldCloseForm;
        }

        #region Tryb naprawczy
        //----------------------------------------------------------------

        /// <summary>
        /// Zmienia aktualny stan bazy danych. Przechodzi w tryb naprawczy, jeśli był ustawiony normalny i vice versa
        /// </summary>
        public void changeEmergencyMode()           // zestaticować?
        {
            if (!EmergencyMode.isEmergency)
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    @"  ALTER DATABASE ProjektBD
                        SET EMERGENCY WITH ROLLBACK IMMEDIATE"          // Przełącza bazę w tryb naprawczy. Dostęp mają tylko najwyżsi admini,
                    );                                                  // w dodatku mogą oni jedynie SELECT'ować.
                                                                        // Dodatkowo rozłącza wszystkich userów i cofa niezacommitowane transakcje
                EmergencyMode.isEmergency = true;
            }

            else
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    @"  ALTER DATABASE ProjektBD
                        SET ONLINE"
                    );

                EmergencyMode.isEmergency = false;
            }
        }

        /// <summary>
        /// Sprawdza, czy baza jest w stanie naprawczym
        /// </summary>
        public void checkEmergencyMode()                // zestaticować?
        {
            using (ProjektBDContext context = new ProjektBDContext())
            {
                string databaseState = context.Database.SqlQuery<string>
                    (@" SELECT state_desc
                        FROM sys.databases
                        WHERE name = 'ProjektBD'").FirstOrDefault();

                if (databaseState.Equals("EMERGENCY"))
                    EmergencyMode.isEmergency = true;
                else
                    EmergencyMode.isEmergency = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        /// <summary>
        /// Zapisuje zmiany dokonane w kontekście
        /// </summary>
        public void saveContext()
        {
            if (context != null)
                context.SaveChanges();
        }

        /// <summary>
        /// Pozbywa się utworzonego kontekstu
        /// </summary>
        public void disposeContext()
        {
            if (context != null)
                context.Dispose();
        }
    }
}
