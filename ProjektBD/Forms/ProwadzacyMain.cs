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
            inputLogin = userLogin;
        }

        /// <summary>
        /// Szukamy nowych zgłoszeń pod naszym adresem.
        /// </summary>
        private void checkForNewApplications()
        {
            notifications.applicationList = formController.getNewApplications(userLogin);
            notifications.applicationCount = notifications.applicationList.Count;

            if (notifications.applicationCount != 0)
            {
                notificationImage.Image = ProjektBD.Properties.Resources.znak;
                notificationCount.Visible = true;

                if (notifications.applicationCount <= 100)
                    notificationCount.Text = notifications.applicationCount.ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            DodajPrzedmiot newForm = new DodajPrzedmiot();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem();
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

    }

    struct TeacherNotifications
    {
        public List<Zgłoszenie> applicationList;
        public int applicationCount;
    }
}
