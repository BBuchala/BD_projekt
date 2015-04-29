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
using ProjektBD.Utilities;
using ProjektBD.Model;

namespace ProjektBD.Forms
{
    public partial class AdministratorMain : Form
    {
        /// <summary>
        /// Zarządza operacjami przeprowadzanymi na bazie danych
        /// </summary>
        private DatabaseUtils database;

        public AdministratorMain()
        {
            InitializeComponent();
            database = new DatabaseUtils();
        }

        private void lookForNewTeachers()
        {
            int newUsers = 0;

            //context.Użytkownicy.Load();

            // Sprawdzanie userów

            if (newUsers != 0)
            {
                notificationImage.Image = global::ProjektBD.Properties.Resources.znak;
                notificationCount.Visible = true;
                notificationCount.Text = newUsers.ToString();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = ProjektBD.Properties.Resources.pressed;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = ProjektBD.Properties.Resources.unpressed;

            if (!EmergencyMode.isEmergency)
            {
                label4.ForeColor = Color.Crimson;
                label4.Text = "wyłączona";
            }
            else
            {
                label4.ForeColor = Color.Chartreuse;
                label4.Text = "włączona";
            }

            database.changeEmergencyMode();
        }

        private void AdministratorMain_Load(object sender, EventArgs e)
        {
            if (database.connectToDB())
                this.Close();

            if (EmergencyMode.isEmergency)
            {
                label4.ForeColor = Color.Crimson;
                label4.Text = "wyłączona";
            }

            else
            {
                label4.ForeColor = Color.Chartreuse;
                label4.Text = "włączona";
            }

            lookForNewTeachers();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
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
            database.disposeContext();
        }
    }
}