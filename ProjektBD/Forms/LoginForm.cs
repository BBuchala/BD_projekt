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
        private ProjektBDContext context;

        // Pierdołowaty String do wyświetlania prostego powitania
        private String inputLogin;

        /*
         * Bool i metoda mówiące, czy zamknięto formatkę za pomocą przycisku X (zamknij aplikację),
         * czy za pomocą poprawnych danych (przejdź do głównej formatki). Działa też na [alt]+[f4]
         */ 
        private bool xButtonClose = true;

        public bool getXButtonClose()
        {
            return xButtonClose;
        }

        public String getInputLogin()
        {
            return inputLogin;
        }


        //***********************************************************************
        // Funkcja tylko do łączenia się z bazą i dająca wybór: spróbuj ponownie
        // lub zamknij formatkę. Utworzona, ponieważ wywoływanie rekurencyjne
        // Form_Load to zły pomysł.
        //***********************************************************************
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

        public LoginForm()
        {
            InitializeComponent();
            context = new ProjektBDContext();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connectToDB();
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

        private void logUser()
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
                        break;
                }

                mainForm.ShowDialog();
                mainForm.Dispose();

                login.Text = "";
                password.Text = "";
                this.Show();
            }
        }

        /********************************************************************************************/
        // Tu wyszukujemy (po naciśnięciu pierwszego przycisku).
        /********************************************************************************************/
        private void loginButton_Click(object sender, EventArgs e)
        {
            logUser();
        }

        private void signButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            RegisterForm mainForm = new RegisterForm();
            mainForm.ShowDialog();
            mainForm.Dispose();

            login.Text = "";
            password.Text = "";
            this.Show();
        }


        /*
         * Wyświetla msgBox z pytaniem o pozostanie w aplikacji.
         */
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
    }
}
