using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjektBD.Databases;
using ProjektBD.Exceptions;

namespace ProjektBD.Controllers
{
    /// <summary>
    /// Kontroler dla formularza rejestracji
    /// </summary>
    class RegistrationController : Controller
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        List<TextBoxBase> textFields;
        List<Label> labels;

        MaskedTextBox index;
        CheckBox birthDate;
        DateTimePicker dateTimePicker1;
        int nrIndeksu;

        RegistrationDatabase regDatabase;

        public RegistrationController(List<TextBoxBase> textFieldsFromForm, List<Label> labelsFromForm, MaskedTextBox indexFromForm,
            CheckBox birthDateFromForm, DateTimePicker dateTimePickerFromForm)
        {
            textFields = textFieldsFromForm;
            labels = labelsFromForm;
            index = indexFromForm;
            birthDate = birthDateFromForm;
            dateTimePicker1 = dateTimePickerFromForm;

            database = new RegistrationDatabase();
            regDatabase = (database as RegistrationDatabase);
        }

        //----------------------------------------------------------------
        #endregion

        /// <summary>
        /// Dokonuje sprawdzenia, czy użytkownik podał prawidłowe dane podczas rejestracji
        /// </summary>
        public string validateInput()
        {
            checkFieldsLength();

            if ( !checkIndexNumber() )
                return "Numer indeksu niekompletny";

            if ( !checkPasswords() )
                return "Różne hasła";

            if ( !checkEmail() )
                return "Niepoprawny email";

            checkOverlapping();

            return createAccount();
        }

        /// <summary>
        /// Sprawdza, czy podane pola nie są za krótkie
        /// </summary>
        void checkFieldsLength()
        {
            // count - 2, bo adres nieobowiązkowy, indeks sprawdzamy osobno
            for (int i = 0; i < textFields.Count - 2; i++)
            {
                if ((textFields[i].Text.Equals("")) || (textFields[i].Text.Length < 3) || (textFields[i].Text == null))
                    throw new EmptyFieldException(i);
            }
        }

        /// <summary>
        /// Sprawdza, czy podany nr indeksu nie jest zbyt krótki
        /// </summary>
        bool checkIndexNumber()
        {
            // Indeks sprawdzamy osobno, bo tylko w niektórych przypadkach
            if (index.Enabled)
            {
                if ( (index.Text.Equals("")) || (index.Text.Length < 3) || (index.Text == null) )
                    throw new EmptyFieldException(4);

                // Czy wszystkie pola w maskedTextBox indeksu są uzupełnione
                if (!index.MaskCompleted)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Sprawdza, czy podane hasła się pokrywają
        /// </summary>
        bool checkPasswords()
        {
            // Jeżeli hasła są różne
            if (textFields[1].Text != textFields[2].Text)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Sprawdza poprawność podanego adresu e-mail
        /// </summary>
        bool checkEmail()
        {
            // Sprawdźmy, czy email jest dobrze podany
            if ( !(SpellCheckUtilities.isValidEmail(textFields[3].Text)) )
                return false;
            else
                return true;
        }

        /// <summary>
        /// Sprawdza, czy w bazie nie istnieje użytkownik o podanym loginie, adresie e-mail lub numerze indeksu
        /// </summary>
        void checkOverlapping()
        {
            if (index.Text.Equals(""))
                nrIndeksu = 0;
            else
                nrIndeksu = Int32.Parse(index.Text);

            // Czy login/email/indeks się nie pokrywa z istniejącym użytkownikiem
            string overlappingAttribute = regDatabase.isOccupied(index.Enabled, textFields[0].Text, textFields[3].Text, nrIndeksu);

            if (!overlappingAttribute.Equals(""))
                throw new UsersOverlappingException(overlappingAttribute);
        }

        /// <summary>
        /// Wysyła do bazy zapytanie SQL tworzące nowego użytkownika
        /// </summary>
        /// <returns> Konto o jakich uprawnieniach zostało utworzone </returns>
        string createAccount()
        {
            if (index.Enabled)
            {
                regDatabase.createStudentAccount(textFields, nrIndeksu, birthDate.Checked, dateTimePicker1.Value);
                return "Utworzono konto studenta";
            }
            else
            {
                regDatabase.notifyAdmin(textFields, birthDate.Checked, dateTimePicker1.Value);
                return "Utworzono konto prowadzącego";
            }
        }
    }
}
