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

        List<Użytkownik> newUsers;

        int newUsersCount;

        public AdministratorMain()
        {
            InitializeComponent();
            database = new DatabaseUtils();
        }

        #region Methods

        /// <summary>
        /// Odświeża informacje o nowych notyfikacjach. Teraz tylko 
        /// szuka nowych userów ubiegających się o prowadzącego.
        /// </summary>
        private void lookForNewTeachers()
        {
            newUsers = database.findUsers();

            newUsersCount = newUsers.Count;

            if (newUsersCount != 0)
            {
                notificationImage.Image = global::ProjektBD.Properties.Resources.znak;
                notificationCount.Visible = true;
                if (newUsersCount <= 100)
                    notificationCount.Text = newUsersCount.ToString();
                else
                    notificationCount.Text = "99+";
            }
            else
            {
                notificationImage.Image = global::ProjektBD.Properties.Resources.znak2;
                notificationCount.Visible = false;
            }
        }

        /// <summary>
        /// Wywołanie msgBoxa dla nowego użytkownika ubiegającego się o prowadzącego.
        /// Można go akceptować (Tak), odrzucić (Nie) lub wybrać później (Anuluj)
        /// </summary>
        /// <param name="u">Rozpatrywany Użytkownik</param>
        private void acceptNewTeacher(Użytkownik u)
        {
            switch (MessageBox.Show("Nowy użytkownik:\n\tLogin: " + u.login + "\n\tE-mail: " + u.email + "\n Ubiega się o uprawnienia prowadzącego. Akceptować?", "Nowy prowadzący",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
            {
                case DialogResult.Yes:
                    database.addTeacher(u);
                    database.deleteUser(u);
                    break;

                case DialogResult.No:
                    database.deleteUser(u);
                    break;

                default:
                    break;
            }
        }

        #endregion 

        # region Events

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

        /// <summary>
        /// Poinformuj ile użytkowników ubiega się o prowadzącego.
        /// W przyszłości może być więcej rodzajów notek, wtedy modyfikujemy dalsze menuItemy.
        /// </summary>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (newUsersCount > 0)
            {
                string str = "";

                str += newUsersCount.ToString() + " nowy" + ((newUsersCount > 1) ? "ch" : "") + " użytkownik" + ((newUsersCount > 1) ? "ów" : "");

                nowyUserToolStripMenuItem.Text = str;
                nowyUserToolStripMenuItem.ForeColor = Color.Red;
            }
            else
            {
                nowyUserToolStripMenuItem.Text = "Brak nowych użytkowników";
                nowyUserToolStripMenuItem.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Menu kontekstowe również pod PPM
        /// </summary>
        private void notificationImage_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Control.MousePosition);
        }

        /// <summary>
        /// Menu kontekstowe również pod PPM
        /// </summary>
        private void notificationCount_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Control.MousePosition);
        }

        /// <summary>
        /// Wywołuje kolejne msgBoxy tak długo jak są nowe zgłoszenia.
        /// </summary>
        private void nowyUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < newUsersCount; i++)
            {
                acceptNewTeacher(newUsers[i]);
            }
            lookForNewTeachers();
        }

        #endregion

    }
}