using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using ProjektBD.DAL;
using ProjektBD.Model;
using ProjektBD.Exceptions;
using ProjektBD;
using ProjektBD.Utilities;
using ProjektBD.Controllers;
using System.Data.Entity.Core;

namespace ProjektBD.Forms
{
    /// <summary>
    /// Formatka służąca do stworzenia nowego konta. Konto studenta jest tworzone automatycznie, prowadzący musi być zaakceptowany przez admina.
    /// </summary>
    public partial class RegisterForm : Form
    {
        #region Fields

        /// <summary>
        /// Lista z textBoxami, aby łatwiej je edytować (np. forEachem).
        /// </summary>
        List<TextBoxBase> textFields = new List<TextBoxBase>();

        /// <summary>
        /// Lista z labelami, aby łatwiej je edytować (np. forEachem).
        /// </summary>
        List<Label> labels = new List<Label>();

        /// <summary>
        /// Bool pozwalający pominąć msgBoxa, jeżeli uda się nam poprawnie założyć konto.
        /// </summary>
        private bool registeredSuccessfully = false;

        /// <summary>
        /// Warstwa pośrednicząca między widokiem a modelem (bazą danych). Przetwarza i oblicza
        /// </summary>
        private RegistrationController formController;

        #endregion

        #region Constructors

        public RegisterForm()
        {
            InitializeComponent();
            formController = new RegistrationController(textFields, labels, index, birthDate, dateTimePicker1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Uzupełnia listy z labelami i TextBoxami. Kolejność jest ważna!
        /// Labele muszą być dodawane w tej samej kolejności co TextBoxy.
        /// Dlaczego - patrz Button_Click.
        /// </summary>
        private void fillLists()
        {
            textFields.Add(login);
            textFields.Add(password1);
            textFields.Add(password2);
            textFields.Add(email);
            textFields.Add(index);
            textFields.Add(address);

            // label1 - tekst informujący, label 7 - ten od daty
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label8);

        }

        #endregion

        #region Events

        /// <summary>
        /// Metoda ładująca formatkę. Ustawiamy sposób maskowania MaskedBoxa oraz domyślny item w comboBoxie.
        /// </summary>
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            if (formController.connectToDatabase())
                this.Close();                               // Nie jestem pewny, czy będzie działać, ale powinno

            // Do pola z indeksem można wpisac tylko 6-cyfrowego inta
            index.Mask = "000000";
            index.ValidatingType = typeof(int);

            comboBox1.SelectedIndex = 1;
            index.Enabled = false;

            fillLists();
        }

        /// <summary>
        /// Uzależniamy dateTimePicker od checkBoxa.
        /// </summary>
        private void birthDate_CheckedChanged(object sender, EventArgs e)
        {
            if (birthDate.Checked == true)
                dateTimePicker1.Enabled = true;
            else
                dateTimePicker1.Enabled = false;
        }

        /// <summary>
        /// Metoda chowająca i disable'ująca nr indeksu (na wszelki wypadek) przy wyborze "Prowadzącego".
        /// </summary>
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                index.Visible = false;
                index.Enabled = false;
                label6.Visible = false;
                this.Height -= 50;
            }
            else
            {
                index.Visible = true;
                index.Enabled = true;
                label6.Visible = true;
                this.Height += 50;
            }
        }

        /// <summary>
        /// Metoda kolorująca button.
        /// </summary>
        private void createButton_MouseEnter(object sender, EventArgs e)
        {
            createButton.BackColor = Color.PaleTurquoise;
            createButton.ForeColor = Color.Black;
        }

        /// <summary>
        /// Metoda kolorująca button.
        /// </summary>
        private void createButton_MouseLeave(object sender, EventArgs e)
        {
            createButton.BackColor = Color.SpringGreen;
            createButton.ForeColor = Color.White;
        }

        /// <summary>
        /// Najważniejsza funkcja - sprawdza szereg warunków przed dodaniem studenta do bazy. 
        /// </summary>
        private void createButton_Click(object sender, EventArgs e)
        {
            // Wszystkie napisy znów na czarno
            foreach (Label lb in labels)
                lb.ForeColor = Color.Black;

            try
            {
                string czyPoprawneDane = formController.validateInput();

                switch (czyPoprawneDane)
                {
                    case "Numer indeksu niekompletny":
                        MsgBoxUtils.displayWarningMsgBox("Zły nr indeksu", "Podany numer indeksu jest niepełny!");

                        labels[4].ForeColor = Color.Red;
                        return;

                    case "Różne hasła":
                        MsgBoxUtils.displayWarningMsgBox("Sprzeczność", "Podane hasła się nie zgadzają!");

                        labels[1].ForeColor = Color.Red;
                        labels[2].ForeColor = Color.Red;

                        textFields[1].Text = "";
                        textFields[2].Text = "";
                        return;

                    case "Niepoprawny email":
                        MsgBoxUtils.displayWarningMsgBox("Zły adres e-mail", "Podany adres e-mail nie jest poprawny!");

                        labels[3].ForeColor = Color.Red;
                        return;

                    case "Utworzono konto studenta":
                        MsgBoxUtils.displayInformationMsgBox("Koniec", "Konto zostało poprawnie założone. Możesz się zalogować.");
                        break;

                    case "Utworzono konto prowadzącego":
                        MsgBoxUtils.displayInformationMsgBox("Koniec", "Konto zostało poprawnie założone. Należy zaczekać na akceptację administratora.");
                        break;
                }

                this.registeredSuccessfully = true;
                this.Close();
            }
            catch (EmptyFieldException err)
            {
                MsgBoxUtils.displayWarningMsgBox("Brak danych", "Pola muszą zawierać co najmniej 3 znaki.");

                labels[err.getFieldNumber()].ForeColor = Color.Red;
            }
            catch (UsersOverlappingException err)
            {
                MsgBoxUtils.displayErrorMsgBox("Złe dane", "Następujący " + err.getMessage() + " jest już zajęty.");
            }
            catch (EntityException)
            {
                MsgBoxUtils.displayConnectionErrorMsgBox();
            }
        }

        /// <summary>
        /// Zamykanie formatki - messageBox z zapytaniem.
        /// </summary>
        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            if (!this.registeredSuccessfully && this.DialogResult == DialogResult.Cancel)
            {
                switch (MsgBoxUtils.displayQuestionMsgBox("Wyjdź", "Jesteś pewien, że chcesz opuścić okno rejestracji?", this))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Zaknięcie formatki - Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        /// </summary>
        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            formController.disposeContext();
        }

        /// <summary>
        /// Wyświetlanie pomocy
        /// </summary>
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            //HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Register);
        }

        #endregion
    }    
}