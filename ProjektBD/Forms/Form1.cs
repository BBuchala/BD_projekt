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
using ProjektBD.Model;
using System.Data.Entity;
using ProjektBD.Utilities;

namespace ProjektBD
{
    public partial class Form1 : Form
    {
        ProjektBDContext context;

        /**
         * Wyświetla tą formatkę jako główne okno, chowie je i wymusza LoginForm na front.
         * Wraca, user się zaloguje, przy okazji bierze sobie nick zalogowanego.
         * Jak dostaje informację, że user na LoginForm użył X lub [alt]+[f4], to
         * zamyka aplikację.
         */ 
        public Form1()
        {
            InitializeComponent();

            context = new ProjektBDContext();

            //this.Hide();                      // niepotrzebne, konstruktor i tak czeka z wyświetleniem, dopóki logowanie nie zwróci wyników
            LoginForm loginForm = new LoginForm();
            
            loginForm.ShowDialog();
            // Gdy nie udało się połaczyć z bazą i wybieramy "nie" (zamknięcie okna), na ułamek sekundy pokazuje się pusta formatka
            // Przydałoby się to naprawić
            
            //if (loginForm.getXButtonClose() == true)
            //    Load += (s, e) => Close();          // dodaje do zdarzenia Form_Load wywołanie metody this.Close()
            //else
            //    label1.Text = "Witaj " + loginForm.getInputLogin() + "!";

            loginForm.Dispose();                    // Wymagane, gdy wywołujemy formatkę przez ShowDialog (wycieki pamięci)
        }

        /* Nowy konstruktor z argumentem dla wersji z LoginForm jako główną formatką */
        public Form1(string userLogin)
        {
            InitializeComponent();

            context = new ProjektBDContext();

            label1.Text = "Witaj " + userLogin + "!";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                context.Database.Initialize(false);

                label1.ForeColor = Color.Green;
                label1.Text = "Połączenie nawiązane!";
            }
            catch (System.Data.SqlClient.SqlException)
            {
                label1.ForeColor = Color.Red;
                label1.Text = "Połączenie nie powiodło się. Upewnij się, że nie jesteś podłączony z bazą danych z innym miejscu.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //--------------------------
            // SELECT ver. 1
            // Zalecany przez dokumentację Microsoft'u
            // Z sortowaniem, yay!
            //--------------------------

            context.Studenci.Load();

            var query = context.Studenci.Local.ToBindingList();
            dataGridView1.DataSource = query;

            // bajery
            dataGridView1.Columns["UżytkownikID"].DisplayIndex = 0;

            dataGridView1.Sort(dataGridView1.Columns["UżytkownikID"], ListSortDirection.Ascending);

            dataGridView1.Columns["email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["miejsceZamieszkania"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //--------------------------
            // SELECT ver. 2 - zapytanie LINQ
            // Najdłuższy, ale mamy pełną władzę nad kolejnością kolumn bez grzebania w atrybutach dataGrid'a
            //--------------------------

            //var query = from b in context.Studenci
            //            select new
            //            {
            //                UżytkownikID = b.UżytkownikID,
            //                login = b.login,
            //                hasło = b.hasło,
            //                email = b.email,
            //                nrIndeksu = b.nrIndeksu,
            //                dataUrodzenia = b.dataUrodzenia,
            //                miejsceZamieszkania = b.miejsceZamieszkania
            //            };

            //dataGridView1.DataSource = query.ToList();
            
            //--------------------------
            // SELECT ver. 3
            // Najkrótszy, ale generuje największe obciążenie dla BD
            //--------------------------

            //var query = from b in context.Studenci
            //            select b;

            //dataGridView1.DataSource = query.ToList();

            //--------------------------
            // SELECT ver. 4
            // Warunkowy z użyciem wyrażenia lambda
            //--------------------------

            //var query = context.Studenci.Where(s => s.miejsceZamieszkania.Equals("Mysłowice"));

            //dataGridView1.DataSource = query.ToList();

            //--------------------------
            // SELECT ver. 5
            // Dla konserwatystów
            //--------------------------

            //// Pobiera konkretną kolumnę
            //var query2 = context.Database.SqlQuery<int>("SELECT UżytkownikID FROM Student").ToList();

            //// Pobiera całą tabelę (z tego co widzę, chyba hejtuje dziedziczenie)
            //var query3 = context.Przedmioty.SqlQuery("SELECT * FROM Przedmiot").ToList();

            //dataGridView1.DataSource = query3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student s = new Student { login = "Korda", hasło = "pedał", email = "Korda@student.projektBD.pl",
                miejsceZamieszkania = "NekoMikoMikołów", nrIndeksu = 219795};

            //--------------------------
            // INSERT ver. 1
            //--------------------------

            Student st = context.Studenci.Where(x => x.nrIndeksu == 219795).FirstOrDefault();

            if (st == null)
            {
                context.Studenci.Add(s);
                context.SaveChanges();
            }

            //--------------------------
            // INSERT ver. 2
            //--------------------------

            //Przedmiot p = context.Przedmioty.Find(4);               // Znajduje przedmiot z kluczem głównym o wartości 4
            //p.Studenci.Add(s);
            //context.SaveChanges();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //--------------------------
            // UPDATE ver. 1
            //--------------------------

            Student s = context.Studenci.Where(x => x.login.Equals("Forczu")).FirstOrDefault();
            s.miejsceZamieszkania = "Rybnik";
            context.SaveChanges();

            dataGridView1.Refresh();

            //--------------------------
            // UPDATE ver. 2
            // @Deprecated, baza rozsynchronizowuje się z kontekstem. Nie stosować, chyba że chcemy coś ukryć
            //--------------------------

            //context.Database.ExecuteSqlCommand("UPDATE Użytkownik SET miejsceZamieszkania = 'Rybnik' where login = 'Forczu'");
            //context.SaveChanges();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (context != null)
                context.Dispose();              // Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        }


        /*
         *  Bliźniaczo podobne do zamykania aplikacji, a działa jak należy :>
         */ 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            if (this.DialogResult == DialogResult.Cancel)
            {
                switch ( MsgBoxUtils.displayQuestionMsgBox("Wyloguj się", "Jesteś pewien, że chcesz się wylogować?", this) )
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception) { }
        }
    }
}
