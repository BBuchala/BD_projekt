using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Utilities;
using ProjektBD.Controllers;
using ProjektBD.Model;
using ProjektBD.Forms.TeacherForms;

namespace ProjektBD.Forms
{
    public partial class ProwadzacyMain : Form
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        /// <summary>
        /// Kontroler do zarządzania i komunikowania się z bazą danych.
        /// </summary>
        TeacherController formController;

        /// <summary>
        /// Login zalogowanego użytkownika, można używać do wyszukiwania.
        /// </summary>
        private string userLogin;

        /// <summary>
        /// Struktura zawierająca informacje o zgłoszeniach dla danego nauczyciela.
        /// </summary>
        private TeacherNotifications notifications;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="inputLogin">Nazwa zalogowanego prowadzącego.</param>
        public ProwadzacyMain(string inputLogin)
        {
            InitializeComponent();
            formController = new TeacherController();
            userLogin = inputLogin;
        }

        //----------------------------------------------------------------
        #endregion

        #region Ładowanie formularza
        //----------------------------------------------------------------

        private void ProwadzacyMain_Load(object sender, EventArgs e)
        {
            if (formController.connectToDatabase())
                this.Close();

            checkForNewApplications();

            new ToolTip().SetToolTip(pictureBox2, "Wyloguj");
        }

        //----------------------------------------------------------------
        #endregion

        #region Zgłoszenia
        //----------------------------------------------------------------

        #region Szukanie zgłoszeń
        //----------------------------------------------------------------

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

        //----------------------------------------------------------------
        #endregion

        #region Otwieranie rozwijalnego menu
        //----------------------------------------------------------------

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

        /// <summary>
        /// Wyświetlanie nowych zgłoszeń w menu kontekstowym.
        /// </summary>
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

        //----------------------------------------------------------------
        #endregion

        #region Zgłoszenia na przedmiot
        //----------------------------------------------------------------

        /// <summary>
        /// Rozpatrzenie zgłoszenia na przedmiot poprzez MsgBox.
        /// </summary>
        /// <param name="app">Rozpatrywane zgłoszenie.</param>
        private void addStudentToSubject(ZgłoszenieNaPrzedmiotDTO app)
        {
            switch (MessageBox.Show("Student" + app.loginStudenta + " o numerze indeksu: " + app.numerIndeksu +
                " chce zapisać się na przedmiot " + app.nazwaPrzedmiotu + ".\n Akceptować zagłoszenie na przedmiot?", "Zapis na przedmiot" + app.nazwaPrzedmiotu,
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
            {
                case DialogResult.Yes:
                    formController.addStudentToSubject(app.IDZgłoszenia);
                    break;

                case DialogResult.No:
                    formController.deleteApplication(app.IDZgłoszenia);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Wyświetlanie zgłoszeń na przedmioty
        /// </summary>
        private void zgłoszeniaNaPrzedmiotToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            zgłoszeniaNaPrzedmiotToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();

            try                            // dopiero tutaj, a nie wewnątrz funkcji, ponieważ połączenie może się zerwać w połowie dodawania prowadzących
            {
                for (int i = 0; i < (notifications.subjectApplicationCount > 10 ? 10 : notifications.subjectApplicationCount); i++) // do 10, żeby menu się nie rozrastało niepotrzebnie
                {
                    ToolStripMenuItem tmp = new ToolStripMenuItem();
                    tmp.Text = "Student " + notifications.subjectApplicationList[i].loginStudenta + " na przedmiot " + notifications.subjectApplicationList[i].nazwaPrzedmiotu;
                    tmp.Tag = notifications.subjectApplicationList[i].loginStudenta + " " + notifications.subjectApplicationList[i].nazwaPrzedmiotu;
                    items.Add(tmp);
                    tmp.Click += new EventHandler(SubjectMenuItemClickHandler);
                }

                zgłoszeniaNaPrzedmiotToolStripMenuItem.DropDownItems.AddRange(items.ToArray());
                zgłoszeniaNaPrzedmiotToolStripMenuItem.DropDown.AllowDrop = true;
            }

            catch (EntityException)
            {
                MsgBoxUtils.displayConnectionErrorMsgBox();
            }
        }

        /// <summary>
        /// Identyfikuje prowadzącego na podstawie przyciśniętego MenuItema
        /// </summary>
        private void SubjectMenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            string[] tags = clickedItem.Tag.ToString().Split(' ');

            foreach (ZgłoszenieNaPrzedmiotDTO application in notifications.subjectApplicationList)
            {
                if (tags[0].Equals(application.loginStudenta) && tags[1].Equals(application.nazwaPrzedmiotu))
                    addStudentToSubject(application);
            }

            checkForNewApplications();
        }

        //----------------------------------------------------------------
        #endregion

        #region Zgłoszenia na projekt
        //----------------------------------------------------------------

        /// <summary>
        /// Rozpatrzenie zgłoszenia na projekt poprzez MsgBox.
        /// </summary>
        /// <param name="app">Rozpatrywane zgłoszenie.</param>
        private void addStudentToProject(ZgłoszenieNaProjektDTO app)
        {
            switch (MessageBox.Show("Student" + app.loginStudenta + " o numerze indeksu: " + app.numerIndeksu +
                " chce zapisać się na projekt " + app.nazwaProjektu + " w ramach przedmiotu " +
                app.nazwaPrzedmiotu + ".\n Akceptować zagłoszenie na projekt?", "Zapis na projekt " + app.nazwaProjektu,
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
            {
                case DialogResult.Yes:
                    formController.addStudentToProject(app.IDZgłoszenia);
                    break;

                case DialogResult.No:
                    formController.deleteApplication(app.IDZgłoszenia);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Wyświetlanie zgłoszeń na projekty
        /// </summary>
        private void zgłoszeniaNaProjektyToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            zgłoszeniaNaProjektyToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();

            try                            // dopiero tutaj, a nie wewnątrz funkcji, ponieważ połączenie może się zerwać w połowie dodawania prowadzących
            {
                for (int i = 0; i < (notifications.projectApplicationCount > 10 ? 10 : notifications.projectApplicationCount); i++) // do 10, żeby menu się nie rozrastało niepotrzebnie
                {
                    ToolStripMenuItem tmp = new ToolStripMenuItem();
                    tmp.Text = "Student " + notifications.projectApplicationList[i].loginStudenta + " na projekt " + notifications.projectApplicationList[i].nazwaProjektu;
                    tmp.Tag = notifications.projectApplicationList[i].loginStudenta + " " + notifications.projectApplicationList[i].nazwaProjektu;
                    items.Add(tmp);
                    tmp.Click += new EventHandler(ProjectMenuItemClickHandler);
                }

                zgłoszeniaNaProjektyToolStripMenuItem.DropDownItems.AddRange(items.ToArray());
                zgłoszeniaNaProjektyToolStripMenuItem.DropDown.AllowDrop = true;
            }

            catch (EntityException)
            {
                MsgBoxUtils.displayConnectionErrorMsgBox();
            }
        }

        /// <summary>
        /// Identyfikuje prowadzącego na podstawie przyciśniętego MenuItema
        /// </summary>
        private void ProjectMenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            string[] tags = clickedItem.Tag.ToString().Split(' ');

            foreach (ZgłoszenieNaProjektDTO application in notifications.projectApplicationList)
            {
                if (tags[0].Equals(application.loginStudenta) && tags[1].Equals(application.nazwaProjektu))
                    addStudentToProject(application);
            }

            checkForNewApplications();
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion    
        
        #region Buttony
        //----------------------------------------------------------------

        private void button2_Click(object sender, EventArgs e)
        {
            DodajPrzedmiot newForm = new DodajPrzedmiot();
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

        //----------------------------------------------------------------
        #endregion

        #region Help i zarządzanie kontem
        //----------------------------------------------------------------

        /// <summary>
        /// Wyświetlanie pomocy
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Teacher);
        }

        /// <summary>
        /// Wyświetlanie pomocy
        /// </summary>
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Teacher);
        }

        /// <summary>
        /// Wyświetlanie "About"
        /// </summary>
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.About);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem(userLogin);
            newForm.ShowDialog();
            newForm.Dispose();
        }

        //----------------------------------------------------------------
        #endregion

        #region Zamykanie formularza
        //----------------------------------------------------------------

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

            DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Wyjdź", "Czy na pewno chcesz się wylogować?", this);

            if (result == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Zamknięcie formatki - Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        /// </summary>
        private void ProwadzacyMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            formController.disposeContext();
        }

        //----------------------------------------------------------------
        #endregion
    }

    /// <summary>
    /// Struktura zawierająca informacje o zgłoszeniach.
    /// </summary>
    struct TeacherNotifications
    {
        public List<ZgłoszenieNaPrzedmiotDTO> subjectApplicationList;
        public int subjectApplicationCount;

        public List<ZgłoszenieNaProjektDTO> projectApplicationList;
        public int projectApplicationCount;
    }
}
