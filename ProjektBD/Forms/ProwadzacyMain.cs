using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Utilities;
using ProjektBD.Controllers;
using ProjektBD.Model;

namespace ProjektBD.Forms
{
    public partial class ProwadzacyMain : Form
    {
        /// <summary>
        /// Kontroler do zarządzania i komunikowania się z bazą danych.
        /// </summary>
        TeacherController formController;

        /// <summary>
        /// Login zalogowanego użytkownika, można używać do wyszukiwania.
        /// </summary>
        private string userLogin;

        private TeacherNotifications notifications;

        public ProwadzacyMain(string inputLogin)
        {
            InitializeComponent();
            formController = new TeacherController();
            userLogin = inputLogin;
        }

        /// <summary>
        /// Szukamy nowych zgłoszeń pod naszym adresem.
        /// </summary>
        private void checkForNewApplications()
        {
            notifications.subjectApplicationList = formController.getSubjectApplications(userLogin);
            notifications.subjectApplicationCount = notifications.subjectApplicationList.Count;

            notifications.projectApplicationList = formController.getProjectApplications(userLogin);
            notifications.projectApplicationCount = notifications.projectApplicationList.Count;

            if (notifications.projectApplicationCount != 0 || notifications.subjectApplicationCount != 0)
            {
                notificationImage.Image = ProjektBD.Properties.Resources.znak;
                notificationCount.Visible = true;

                if (notifications.projectApplicationCount + notifications.subjectApplicationCount <= 100)
                    notificationCount.Text = (notifications.projectApplicationCount + notifications.subjectApplicationCount).ToString();
                else
                    notificationCount.Text = "99+";
            }
            else
            {
                notificationImage.Image = ProjektBD.Properties.Resources.znak2;
                notificationCount.Visible = false;
            }
        }



        #region Events

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (notifications.projectApplicationCount > 0)
            {
                string str = "";

                str += notifications.projectApplicationCount.ToString() + " now" + ((notifications.projectApplicationCount > 1) ? "ych" : "e") +
                    " zgłosze" + ((notifications.projectApplicationCount > 1) ? "ń" : "nie") + " na projekt" +
                    ((notifications.projectApplicationCount > 1) ? "y" : "");

                zgłoszeniaNaProjektyToolStripMenuItem.Text = str;
                zgłoszeniaNaProjektyToolStripMenuItem.ForeColor = Color.Red;
            }
            else
            {
                zgłoszeniaNaProjektyToolStripMenuItem.Text = "Brak nowych zapisów na projekty";
                zgłoszeniaNaProjektyToolStripMenuItem.ForeColor = Color.Black;
            }
            if (notifications.subjectApplicationCount > 0)
            {
                string str = "";

                str += notifications.subjectApplicationCount.ToString() + " now" + ((notifications.subjectApplicationCount > 1) ? "ych" : "e") +
                    " zgłosze" + ((notifications.subjectApplicationCount > 1) ? "ń" : "nie") + " na przedmiot" +
                    ((notifications.subjectApplicationCount > 1) ? "y" : "");

                zgłoszeniaNaPrzedmiotToolStripMenuItem.Text = str;
                zgłoszeniaNaPrzedmiotToolStripMenuItem.ForeColor = Color.Red;
            }
            else
            {
                zgłoszeniaNaPrzedmiotToolStripMenuItem.Text = "Brak nowych zapisów na przedmioty";
                zgłoszeniaNaPrzedmiotToolStripMenuItem.ForeColor = Color.Black;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DodajPrzedmiot newForm = new DodajPrzedmiot();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem(userLogin);
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EdytujPrzedmioty newForm = new EdytujPrzedmioty();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DodajEdytujProjekt newForm = new DodajEdytujProjekt();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DodajEdytujProjekt newForm = new DodajEdytujProjekt();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Zamykanie i wyświetlenie MsgBoxa z zapytaniem.
        /// </summary>
        private void ProwadzacyMain_FormClosing(object sender, FormClosingEventArgs e)
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
        private void ProwadzacyMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        #endregion

        private void ProwadzacyMain_Load(object sender, EventArgs e)
        {
            if (formController.connectToDatabase())
                this.Close();

            checkForNewApplications();

            new ToolTip().SetToolTip(pictureBox2, "Wyloguj");
        }

        /// <summary>
        /// Menu kontekstowe również pod LPM
        /// </summary>
        private void notificationCount_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Control.MousePosition);
        }

        /// <summary>
        /// Menu kontekstowe również pod LPM
        /// </summary>
        private void notificationImage_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Control.MousePosition);
        }


    }

    struct TeacherNotifications
    {
        public List<Zgłoszenie> subjectApplicationList;
        public int subjectApplicationCount;

        public List<Zgłoszenie> projectApplicationList;
        public int projectApplicationCount;
    }
}
