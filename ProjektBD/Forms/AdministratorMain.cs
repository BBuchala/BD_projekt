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
using System.Data.Entity.Core;
using ProjektBD.DAL;
using ProjektBD.Utilities;
using ProjektBD.Model;
using ProjektBD.Controllers;

namespace ProjektBD.Forms
{
    public partial class AdministratorMain : Form
    {
        /// <summary>
        /// Warstwa pośrednicząca między widokiem a modelem (bazą danych). Przetwarza i oblicza
        /// </summary>
        private AdminController formController;

        private Notifications notifications;

        public AdministratorMain()
        {
            InitializeComponent();
            formController = new AdminController();
        }

        #region Methods

        /// <summary>
        /// Odświeża informacje o nowych notyfikacjach. Teraz tylko 
        /// szuka nowych userów ubiegających się o prowadzącego.
        /// </summary>
        private void lookForNewTeachers()
        {
            try
            {
                notifications.newUsers = formController.findNewUsers();
                notifications.newUsersCount = notifications.newUsers.Count;

                if (notifications.newUsersCount != 0)
                {
                    notificationImage.Image = ProjektBD.Properties.Resources.znak;
                    notificationCount.Visible = true;

                    if (notifications.newUsersCount <= 100)
                        notificationCount.Text = notifications.newUsersCount.ToString();
                    else
                        notificationCount.Text = "99+";
                }
                else
                {
                    notificationImage.Image = ProjektBD.Properties.Resources.znak2;
                    notificationCount.Visible = false;
                }
            }

            catch (EntityException)
            {
                MsgBoxUtils.displayConnectionErrorMsgBox();
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
                    formController.addTeacher(u);
                    break;

                case DialogResult.No:
                    formController.deleteUser(u);
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

            try
            {
                formController.changeEmergencyMode();

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
            }

            catch (EntityException)
            {
                MsgBoxUtils.displayConnectionErrorMsgBox();
            }
        }

        private void AdministratorMain_Load(object sender, EventArgs e)
        {
            if ( formController.connectToDatabase() )
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

            new ToolTip().SetToolTip(pictureBox2, "Wyloguj");

            comboBox1.Items.AddRange( formController.getTableNames() );
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        /// <summary>
        /// Poinformuj ile użytkowników ubiega się o prowadzącego.
        /// W przyszłości może być więcej rodzajów notek, wtedy modyfikujemy dalsze menuItemy.
        /// </summary>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (notifications.newUsersCount > 0)
            {
                string str = "";

                str += notifications.newUsersCount.ToString() + " nowy" + ((notifications.newUsersCount > 1) ? "ch" : "") + " użytkownik" + ((notifications.newUsersCount > 1) ? "ów" : "");

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
        /// Dynamicznie ładuje listę nowych użytkowników ubiegających się o prowadzącego
        /// </summary>
        private void nowyUserToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            nowyUserToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();

            try                            // dopiero tutaj, a nie wewnątrz funkcji, ponieważ połączenie może się zerwać w połowie dodawania prowadzących
            {
                for (int i = 0; i < (notifications.newUsersCount > 10 ? 10 : notifications.newUsersCount); i++) // do 10, żeby menu się nie rozrastało niepotrzebnie
                {
                    ToolStripMenuItem tmp = new ToolStripMenuItem();
                    tmp.Text = notifications.newUsers[i].login;
                    items.Add(tmp);
                    tmp.Click += new EventHandler(MenuItemClickHandler);
                }

                nowyUserToolStripMenuItem.DropDownItems.AddRange(items.ToArray());
                nowyUserToolStripMenuItem.DropDown.AllowDrop = true;
            }

            catch (EntityException)
            {
                MsgBoxUtils.displayConnectionErrorMsgBox();
            }
        }

        /// <summary>
        /// Identyfikuje prowadzącego na podstawie przyciśniętego MenuItema
        /// </summary>
        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            foreach (Użytkownik u in notifications.newUsers)
            {
                if (clickedItem.Text == u.login)
                    acceptNewTeacher(u);
            }

            lookForNewTeachers();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var query = await formController.getTableData(comboBox1.Text);

                dataGridView1.DataSource = query;

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;              // Autosize szerokości kolumn
                }
            } 
            catch (System.NotSupportedException) {}     // Niweluje error wyskakujący, gdy asynchroniczne operacje wykonują się zbyt szybko
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: zmiana wartości w kontrolce zmienia wartość w BD
            int x = 2;
        }

        /// <summary>
        /// Zamykanie formatki - messageBox z zapytaniem.
        /// </summary>
        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            if (this.DialogResult == DialogResult.Cancel)           // czy ten warunek jest konieczny?
            {
                DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Wyjdź", "Czy na pewno chcesz się wylogować?", this);

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Zamknięcie formatki - Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        /// </summary>
        private void AdministratorMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            formController.disposeContext();
        }

        #endregion
    }

    struct Notifications
    {
        public List<Użytkownik> newUsers;
        public int newUsersCount;
    }
}