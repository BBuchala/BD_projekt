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

namespace ProjektBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new ProjektBDContext())
            {
                try
                {
                    db.Database.Initialize(false);

                    label1.ForeColor = Color.Green;
                    label1.Text = "Połączenie nawiązane!";

                    Zakład z = new Zakład { nazwa = "ZMiTAC" };
                    db.Zakłady.Add(z);              // Dodaje zakład do bazy
                    db.SaveChanges();               // Commit

                    Prowadzący p = new Prowadzący   // ID = 7
                    {
                        ZakładID = 1,
                        login = "Drabik",
                        hasło = "układ cyfrowy",
                        email = "gabi@polsl.pl",
                        nazwaZakładu = "ZMiTAC"
                    };
                    db.Prowadzący.Add(p);
                    db.SaveChanges();

                    Student s = new Student
                    {
                        nrIndeksu = 219766,
                        login = "Forczu",
                        hasło = "kotori1",
                        email = "SM6969@4chan.org"
                    };
                    db.Studenci.Add(s);
                    db.SaveChanges();

                    Przedmiot przedm = new Przedmiot { ProwadzącyID = 7, nazwa = "TUC", liczbaStudentów = 69 };
                    przedm.Studenci.Add(s);         // Dodaje studenta do przedmiotu
                    db.Przedmioty.Add(przedm);
                    db.SaveChanges();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    label1.ForeColor = Color.Red;
                    label1.Text = "Połączenie nie powiodło się. Upewnij się, że nie jesteś podłączony z bazą danych z innym miejscu.";
                }
            }
        }
    }
}
