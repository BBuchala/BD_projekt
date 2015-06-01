using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

using System.Data.Entity;
using ProjektBD.DAL;
using ProjektBD.Model;
using ProjektBD.Forms;
using ProjektBD.Utilities;
using ProjektBD.Controllers;
using System.Data.Entity.Core;

namespace ProjektBD
{
    /// <summary>
    /// Formatka służąca do logowania. Pozwala się zalogować, wyjść z programu lub przejść do utworzenia nowego konta.
    /// </summary>
    public partial class LoginForm : Form
    {
        #region Fields

        /// <summary>
        /// Ile razy nastąpi zmiana koloru migotającego label'a
        /// </summary>
        private const byte flashLimit = 6;

        /// <summary>
        /// Licznik mignięć
        /// </summary>
        private byte flashCounter = 0;

        /// <summary>
        /// Pierdołowaty String do wyświetlania prostego powitania
        /// </summary>
        private String inputLogin;

        /// <summary>
        /// Zarządza operacjami przeprowadzanymi na bazie danych
        /// </summary>
        //private DatabaseUtils database;

        /// <summary>
        /// Warstwa pośrednicząca między widokiem a modelem (bazą danych). Przetwarza i oblicza
        /// </summary>
        private  AccountController formController;

        #endregion

        #region Constructor and methods

        /// <summary>
        /// Pobiera login, za którego pomocą zalogowano się do systemu. Na chwilę obecną @Deprecated
        /// </summary>
        public String getInputLogin()
        {
            return inputLogin;
        }

        /// <summary>
        /// Sprawdza, czy użytkownik istnieje w bazie i loguje go, otwierając stosowny do uprawnień formularz
        /// </summary>
        private void logUser()
        {
            if (!backgroundWorker1.IsBusy)
            {
                inputLogin = login.Text;

                try
                {
                    string userType = formController.validateUser(login.Text, password.Text);

                    switch (userType)
                    {
                        case "":
                            MsgBoxUtils.displayWarningMsgBox("Błędne dane", "Podane dane są niepoprawne. Spróbuj ponownie.");
                            break;

                        case "Użytkownik":
                            MsgBoxUtils.displayWarningMsgBox("Cierpliwości", "Poczekaj na zatwierdzenie konta przez administratora");
                            break;

                        default:
                            Form mainForm = formController.openUserForm(userType, login.Text);

                            if (mainForm == null)         // jeśli baza jest w stanie naprawczym
                                EmergencyMode.notifyAboutEmergencyMode();

                            else
                            {
                                this.Hide();

                                mainForm.ShowDialog();
                                mainForm.Dispose();
                                                             

                               // pomyśleć nad przeładowaniem wszystkich kontekstów

                                login.Text = "";

                                this.Show();
                            }

                            break;
                    }
                }
                catch (EntityException)
                {
                    MsgBoxUtils.displayConnectionErrorMsgBox();
                }

                password.Text = "";
            }

            else
                timer1.Start();                 // Uruchamia zdarzenie timera - migotanie tekstu
        }

        public LoginForm()
        {
            InitializeComponent();
            formController = new AccountController();
        }

        #endregion

        #region Events

        private void Form2_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();     //Uruchamia drugi wątek, w którym następuje połączenie się z bazą
        }

        private void loginButton_MouseEnter(object sender, EventArgs e)
        {
            loginButton.BackColor = Color.PaleTurquoise;
            loginButton.ForeColor = Color.Black;
        }

        private void loginButton_MouseLeave(object sender, EventArgs e)
        {
            loginButton.BackColor = Color.SpringGreen;
            loginButton.ForeColor = Color.White;
        }

        private void signButton_MouseEnter(object sender, EventArgs e)
        {
            signButton.BackColor = Color.PaleTurquoise;
            signButton.ForeColor = Color.Black;
        }

        private void signButton_MouseLeave(object sender, EventArgs e)
        {
            signButton.BackColor = Color.Red;
            signButton.ForeColor = Color.White;
        }

        /// <summary>
        /// Tu wyszukujemy (po naciśnięciu pierwszego przycisku)
        /// </summary>
        private void loginButton_Click(object sender, EventArgs e)
        {
            logUser();
        }

        /// <summary>
        /// Rejestracja
        /// </summary>
        private void signButton_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                try
                {
                    formController.checkEmergencyMode();

                    if (!EmergencyMode.isEmergency)
                    {
                        this.Hide();

                        RegisterForm mainForm = new RegisterForm();
                        mainForm.ShowDialog();
                        mainForm.Dispose();

                        login.Text = "";
                        password.Text = "";
                        this.Show();
                    }

                    else
                        EmergencyMode.notifyAboutEmergencyMode();
                }

                catch (EntityException)
                {
                    MsgBoxUtils.displayConnectionErrorMsgBox();
                }
            }

            else
                timer1.Start();
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)            // Enter
            {
                logUser();
                e.Handled = true;           // wyłącza beep po powrocie do formularza
            }
        }

        private void login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)            // Enter
            {
                logUser();
                e.Handled = true;           // wyłącza beep po powrocie do formularza
            }
        }

        /// <summary>
        /// Zmienia kolor label'a informującego o połączeniu z bazą. Ilość mignięć definiowana zmienną flashLimit
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ( flashCounter < flashLimit && !label6.ForeColor.Equals(Color.Green))
            {
                if ( label6.ForeColor.Equals(Color.Black) )
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;

                flashCounter++;
            }
            else
            {
                flashCounter = 0;
                timer1.Stop();
            }
        }

        /// <summary>
        /// Zadania wykonywane w ramach drugiego wątku
        /// </summary>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if ( formController.connectToDatabase() )
                backgroundWorker1.RunWorkerCompleted += (s, ev) => Close();
        }

        /// <summary>
        /// Uruchamiana, gdy wątek zakończy swą pracę
        /// </summary>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( formController.connectionSuccessful() )
            {
                label6.Text = "Połączenie zostało nawiązane!";
                label6.ForeColor = Color.Green;

                pictureBox1.Image = ProjektBD.Properties.Resources.OK;
            }
            else
            {
                label6.Text = "Połączenie nie zostało nawiązane!";
                label6.ForeColor = Color.Red;

                pictureBox1.Image = ProjektBD.Properties.Resources.error;
            }
        }

        /// <summary>
        /// Wyświetla msgBox z pytaniem o pozostanie w aplikacji
        /// </summary>
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Zamknij aplikację", "Jesteś pewien, że chcesz zakończyć\npracę z tą aplikacją?", this);
            
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            formController.disposeContext();
        }

        #endregion
    }
}
