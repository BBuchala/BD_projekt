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

using ProjektBD.DAL;
using ProjektBD.Model;
using System.Data.Entity;
using ProjektBD.Exceptions;
using ProjektBD.Forms;

namespace ProjektBD
{
    public partial class LoginForm : Form
    {
        #region Pola

        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        private ProjektBDContext context;

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
        /// Określa, czy zamknięto formatkę za pomocą przycisku X (zamknij aplikację),
        /// czy za pomocą poprawnych danych (przejdź do głównej formatki). Działa też na [alt]+[f4]
        /// </summary>
        private bool xButtonClose = true;

        #endregion
        #region Metody i konstruktor

        /// <summary>
        /// Określa, czy zamknięto formatkę za pomocą przycisku X (zamknij aplikację), czy
        /// za pomocą poprawnych danych (przejdź do głównej formatki). Działa też na [alt]+[f4]
        /// Na chwilę obecną @Deprecated
        /// </summary>
        public bool getXButtonClose()
        {
            return xButtonClose;
        }

        /// <summary>
        /// Pobiera login, za którego pomocą zalogowano się do systemu. Na chwilę obecną @Deprecated
        /// </summary>
        public String getInputLogin()
        {
            return inputLogin;
        }

        /// <summary>
        /// Funkcja tylko do łączenia się z bazą i dająca wybór: spróbuj ponownie
        /// lub zamknij formatkę. Utworzona, ponieważ wywoływanie rekurencyjne
        /// Form_Load to zły pomysł.
        /// </summary>
        private void connectToDB()
        {
            try
            {
                context.Database.Initialize(false);
                context.Użytkownicy.Load();                 // Wczytuje do lokalnej kolekcji wszystkich użytkowników (w tym studentów, prowadzących itp.)
            }
            catch (System.Data.SqlClient.SqlException)
            {
                DialogResult connRetry = MessageBox.Show("Nastąpił błąd podczas próby połączenia z bazą danych.\n Upewnij się, czy nie jesteś połączony w innym miejscu. \n Spróbować ponownie?",
                                                       "Błąd połączenia",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Exclamation);
                if (connRetry == DialogResult.No)
                    this.Close();
                else
                    connectToDB();
            }
        }

        /// <summary>
        /// Sprawdza, czy użytkownik istnieje w bazie i loguje go, otwierając stosowny do uprawnień formularz
        /// </summary>
        private void logUser()
        {
            if (!backgroundWorker1.IsBusy)
            {
                inputLogin = login.Text;
                String inputPass = password.Text;

                // FirstOrDefault zwraca pierwszy wynik zapytania lub null, jeśli użytkownik nie został znaleziony
                Użytkownik query = context.Użytkownicy.Local.Where(s => (s.login.Equals(inputLogin) && s.hasło.Equals(inputPass))).FirstOrDefault();

                if (query == null)
                {
                    MessageBox.Show("Podane dane są niepoprawne. Spróbuj ponownie.",
                                    "Błędne dane.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);

                    //login.Text = "";               // czasem ktoś wklepie źle tylko hasło, tak będzie przyjaźniej dla użytkownika
                    password.Text = "";
                }
                else
                {
                    xButtonClose = false;
                    // Wersja z Form1 jako główną formatką

                    // this.Hide();                     

                    /* Wersja z LoginForm jako główną formatką 
                        * Chowamy, wywołujemy Form1 i nie wracamy tu,
                        * dopóki nie zamknie się Form1. Wtedy zerujemy 
                        * textboxy (dane z logowania zostają) + pokazujemy.
                        */

                    this.Hide();

                    Form mainForm;

                    switch (query.GetType().Name)
                    {
                        case "Administrator":
                            mainForm = new AdministratorMain();
                            break;
                        case "Prowadzący":
                            mainForm = new ProwadzacyMain();
                            break;
                        case "Student":
                            mainForm = new StudentMain();
                            break;
                        default:
                            mainForm = new Form();
                            MessageBox.Show(this, "Błąd w metodzie logUser(). Nieprawidłowy typ encji", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    mainForm.ShowDialog();
                    mainForm.Dispose();

                    login.Text = "";
                    password.Text = "";
                    this.Show();
                }
            }

            else
                timer1.Start();                 // Uruchamia zdarzenie timera - migotanie tekstu

        }

        public LoginForm()
        {
            InitializeComponent();
            context = new ProjektBDContext();
        }

        #endregion
        #region Eventy

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
                this.Hide();

                RegisterForm mainForm = new RegisterForm();
                mainForm.ShowDialog();
                mainForm.Dispose();

                login.Text = "";
                password.Text = "";
                this.Show();
            }

            else
                timer1.Start();
        }

        /// <summary>
        /// Wyświetla msgBox z pytaniem o pozostanie w aplikacji
        /// </summary>
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            //if (this.DialogResult == DialogResult.Cancel)            Kuźwa, przestało działać (kaj mój MsgBox)?!? W Form1 jest ok...
            {
                switch (MessageBox.Show(this, "Jesteś pewien, że chcesz zakończyć\npracę z tą aplikacją?", "Zamknij aplikację", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (context != null)
                context.Dispose();          // Pozbywa się utworzonego kontekstu przy zamykaniu formularza - do wywalenia przy większej ilości formatek.
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

        // TODO: Przerobić wszystko na pojedynczą kontrolkę dodawaną do każdego formularza
        //-------------------------------------------

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
            connectToDB();
        }

        /// <summary>
        /// Uruchamiana, gdy wątek zakończy swą pracę
        /// </summary>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label6.Text = "Połączenie zostało nawiązane!";
            label6.ForeColor = Color.Green;

            pictureBox1.Image = ProjektBD.Properties.Resources.OK;
        }

        //-------------------------------------------
        #endregion
    }
}
