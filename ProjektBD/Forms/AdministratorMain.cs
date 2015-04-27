using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.DAL;
using System.Data.Entity;

namespace ProjektBD.Forms
{
    public partial class AdministratorMain : Form
    {
        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        private ProjektBDContext context;

        public AdministratorMain()
        {
            InitializeComponent();
            context = new ProjektBDContext();
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

            catch (System.Data.DataException)
            {
                MessageBox.Show("Baza danych jest obecnie wyłączona. Proszę spróbować później", "Prace konserwacyjne",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = ProjektBD.Properties.Resources.pressed;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = ProjektBD.Properties.Resources.unpressed;

            if (label4.ForeColor.Equals(Color.Chartreuse))
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, 
                    @"ALTER DATABASE ProjektBD
                        SET EMERGENCY WITH ROLLBACK IMMEDIATE"          // Przełącza bazę w tryb naprawczy. Dostęp mają tylko najwyżsi admini,
                    );                                                  // w dodatku mogą oni jedynie SELECT'ować.
                                                                        // Dodatkowo rozłącza wszystkich userów i cofa niezacommitowane transakcje
                label4.ForeColor = Color.Crimson;
                label4.Text = "wyłączona";
            }

            else
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    @"ALTER DATABASE ProjektBD
                        SET ONLINE"
                    );

                label4.ForeColor = Color.Chartreuse;
                label4.Text = "włączona";
            }
        }

        private void AdministratorMain_Load(object sender, EventArgs e)
        {
            connectToDB();
        }

        /// <summary>
        /// Zamykanie formatki - messageBox z zapytaniem.
        /// </summary>
        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            if (this.DialogResult == DialogResult.Cancel)
            {
                switch (MessageBox.Show(this, "Jesteś pewien, że chcesz opuścić okno rejestracji?", "Wyjdź", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
        private void AdministratorMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (context != null)
                context.Dispose();          // Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        }
    }
}
