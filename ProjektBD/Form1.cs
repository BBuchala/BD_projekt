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
