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
using System.IO;

using ProjektBD.Utilities;
using ProjektBD.Controllers;
using ProjektBD.Model;
using ProjektBD.Forms.TeacherForms;
using ProjektBD.Custom_Controls;

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
        /// Słownik przechowujący zmapowane wartości ID ocen i ich indeksów w kontrolce CustomListView
        /// <para> Karta: Oceny -> Usuń </para>
        /// </summary>
        private Dictionary<int, long> gradeDictionary = new Dictionary<int, long>();          // <indeks w kontrolce, ID oceny>

        /// <summary>
        /// Słownik przechowujący zmapowane wartości ID ocen i ich indeksów w kontrolce CustomListView
        /// <para> Karta: Oceny -> Modyfikuj </para>
        /// </summary>
        private Dictionary<int, long> gradeDictionary2 = new Dictionary<int, long>();          // <indeks w kontrolce, ID oceny>

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="inputLogin">Nazwa zalogowanego prowadzącego.</param>
        public ProwadzacyMain(string inputLogin)
        {
            InitializeComponent();

            userLogin = inputLogin;
            formController = new TeacherController(inputLogin);
        }

        //----------------------------------------------------------------
        #endregion

        #region Ładowanie formularza
        //----------------------------------------------------------------

        private void ProwadzacyMain_Load(object sender, EventArgs e)
        {
            if (formController.connectToDatabase())
                this.Close();

            label25.Text = userLogin;

            checkForNewApplications();

            new ToolTip().SetToolTip(pictureBox2, "Wyloguj");

            List<PrzedmiotDTO> subjectsList = formController.getSubjects();
            List<PrzedmiotProwadzącegoDTO> mySubjectsList = formController.getMySubjects();

            customListView1.fill<PrzedmiotDTO>(subjectsList);
            customListView5.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView8.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView11.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView14.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView17.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
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

            //string[] tags = clickedItem.Tag.ToString().Split(' ');
            string selectedText = clickedItem.Text;
            string[] tags = new string[2];

            selectedText = selectedText.Remove(0, 8);                       // usunięcie "Student "
            tags[0] = selectedText.Split(' ')[0];

            selectedText = selectedText.Remove(0, tags[0].Length + 1);      // usunięcie loginu studenta
            selectedText = selectedText.Remove(0, 13);                      // usunięcie "na projekt "
            tags[1] = selectedText;

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

            //string[] tags = clickedItem.Tag.ToString().Split(' ');
            string selectedText = clickedItem.Text;
            string[] tags = new string[2];

            selectedText = selectedText.Remove(0, 8);                       // usunięcie "Student "
            tags[0] = selectedText.Split(' ')[0];

            selectedText = selectedText.Remove(0, tags[0].Length + 1);      // usunięcie loginu studenta
            selectedText = selectedText.Remove(0, 11);                      // usunięcie "na projekt "
            tags[1] = selectedText;

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

        #region Obsługa customListView'ów
        //----------------------------------------------------------------

        #region listView1 (Podgląd -> Lista przedmiotów)
        //----------------------------------------------------------------

        private void customListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView1.SelectedItems.Count > 0)
            {
                string subjectName = e.Item.Text;

                List<ProjektDTO> projectsList = formController.getProjects(subjectName);
                List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

                customListView2.fill<ProjektDTO>(projectsList);
                customListView3.fill<StudentDTO>(studentsList);
            }
            else
            {
                customListView2.Clear();
                customListView3.Clear();
            }
        }

        private void customListView1_Enter(object sender, EventArgs e)
        {
            if (customListView1.SelectedItems.Count > 0)
            {
                string subjectName = customListView1.SelectedItems[0].Text;

                List<ProjektDTO> projectsList = formController.getProjects(subjectName);
                List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

                customListView2.fill<ProjektDTO>(projectsList);
                customListView3.fill<StudentDTO>(studentsList);
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listView2 (Podgląd -> Lista projektów)
        //----------------------------------------------------------------

        private void customListView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView2.SelectedItems.Count > 0)
            {
                List<StudentDTO> studentsList = formController.getStudentsFromProject(e.Item.Text);

                customListView3.fill<StudentDTO>(studentsList);
            }
            else
            {
                List<StudentDTO> studentsList = formController.getStudentsFromSubject(customListView1.SelectedItems[0].Text);

                customListView3.fill<StudentDTO>(studentsList);
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listView5 (Moje przedmioty i projekty -> Lista przedmiotów)
        //----------------------------------------------------------------

        private void customListView5_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView5.SelectedItems.Count > 0)
            {
                string subjectName = e.Item.Text;

                List<ProjektDTO> projectsList = formController.getProjects(subjectName);
                List<StudentDTO> studentsFromSubjectList = formController.getStudentsFromSubject(subjectName);

                customListView6.fill<ProjektDTO>(projectsList);
                customListView7.fill<StudentDTO>(studentsFromSubjectList);

                button4.Enabled = true;
                button5.Enabled = true;
                button8.Enabled = true;
            }
            else
            {
                customListView6.Clear();
                customListView7.Clear();

                button4.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
            }
        }

        private void customListView5_Enter(object sender, EventArgs e)
        {
            button9.Enabled = false;
            button11.Enabled = false;

            if (customListView5.SelectedItems.Count > 0)
            {
                string selectedSubjectName = customListView5.SelectedItems[0].Text;

                List<ProjektDTO> projectsList = formController.getProjects(selectedSubjectName);
                List<StudentDTO> studentsFromSubjectList = formController.getStudentsFromSubject(selectedSubjectName);

                customListView6.fill<ProjektDTO>(projectsList);
                customListView7.fill<StudentDTO>(studentsFromSubjectList);
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview6 (Moje przedmioty i projekty -> Lista projektów)
        //----------------------------------------------------------------

        private void customListView6_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView6.SelectedItems.Count > 0)
            {
                List<StudentDTO> studentsList = formController.getStudentsFromProject(e.Item.Text);
                customListView7.fill<StudentDTO>(studentsList);

                button9.Enabled = true;
                button11.Enabled = true;
            }
            else
            {
                List<StudentDTO> studentsList = formController.getStudentsFromSubject(customListView5.SelectedItems[0].Text);
                customListView7.fill<StudentDTO>(studentsList);

                button9.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview7 (Moje przedmioty i projekty -> Lista studentów)
        //----------------------------------------------------------------

        private void customListView7_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView7.SelectedItems.Count > 0)
                button12.Enabled = true;
            else
                button12.Enabled = false;
        }

        //----------------------------------------------------------------
        #endregion

        #region listview8 (Oceny -> Dodaj -> Wybierz przedmiot)
        //----------------------------------------------------------------

        private void customListView8_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView8.SelectedItems.Count > 0)
            {
                string subjectName = e.Item.Text;

                List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

                customListView9.fill<StudentDTO>(studentsList);
            }
            else
            {
                customListView9.Clear();
                customListView10.Clear();

                button6.BackColor = Color.LightGray;
                button6.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview9 (Oceny -> Dodaj -> Wybierz studenta)
        //----------------------------------------------------------------

        private void customListView9_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView9.SelectedItems.Count > 0)
            {
                string studentIndexNumber = customListView9.SelectedItems[0].Text;

                List<ForeignProjektDTO> projectsList = formController.getStudentProjects(studentIndexNumber, customListView8.SelectedItems[0].Text);
                customListView10.fill<ForeignProjektDTO>(projectsList);

                if (comboBox1.SelectedItem != null)
                {
                    button6.BackColor = Color.Lime;
                    button6.Enabled = true;
                }
            }
            else
            {
                customListView10.Clear();

                button6.BackColor = Color.LightGray;
                button6.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview11 (Oceny -> Usuń -> Wybierz przedmiot)
        //----------------------------------------------------------------

        private void customListView11_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView11.SelectedItems.Count > 0)
            {
                string subjectName = e.Item.Text;

                List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

                customListView12.fill<StudentDTO>(studentsList);
            }
            else
            {
                customListView12.Clear();
                customListView13.Clear();

                button7.BackColor = Color.LightGray;
                button7.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview12 (Oceny -> Usuń -> Wybierz studenta)
        //----------------------------------------------------------------

        private void customListView12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customListView12.SelectedItems.Count > 0)
            {
                string studentLogin = customListView12.SelectedItems[0].SubItems[1].Text;       // Może bugować po zmianie klasy StudentDTO
                string subjectName = customListView11.SelectedItems[0].Text;

                List<OcenaDTO> gradesList = formController.getGradesFromSubject(studentLogin, subjectName);
                customListView13.fill<OcenaDTO>(gradesList);

                gradeDictionary.Clear();
                for (int i = 0; i < gradesList.Count; i++)
                    gradeDictionary.Add(i, gradesList[i].ocenaID);
            }
            else
            {
                customListView13.Clear();

                button7.BackColor = Color.LightGray;
                button7.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview13 (Oceny -> Usuń -> Wybierz ocenę do usunięcia)
        //----------------------------------------------------------------

        private void customListView13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customListView13.SelectedItems.Count > 0)
            {
                int index = customListView13.SelectedItems[0].Index;
                customListView13.gradeID = gradeDictionary[index];

                button7.BackColor = Color.Red;
                button7.Enabled = true;
            }
            else
            {
                button7.BackColor = Color.LightGray;
                button7.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview14 (Oceny -> Modyfikuj -> Wybierz przedmiot)
        //----------------------------------------------------------------

        private void customListView14_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView14.SelectedItems.Count > 0)
            {
                string subjectName = e.Item.Text;

                List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

                customListView15.fill<StudentDTO>(studentsList);
            }
            else
            {
                customListView15.Clear();
                customListView16.Clear();

                button1.BackColor = Color.LightGray;
                button1.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview15 (Oceny -> Modyfikuj -> Wybierz studenta)
        //----------------------------------------------------------------

        private void customListView15_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView15.SelectedItems.Count > 0)
            {
                string studentLogin = customListView15.SelectedItems[0].SubItems[1].Text;       // Może bugować po zmianie klasy StudentDTO
                string subjectName = customListView14.SelectedItems[0].Text;

                List<OcenaDTO> gradesList = formController.getGradesFromSubject(studentLogin, subjectName);
                customListView16.fill<OcenaDTO>(gradesList);

                gradeDictionary2.Clear();
                for (int i = 0; i < gradesList.Count; i++)
                    gradeDictionary2.Add(i, gradesList[i].ocenaID);
            }
            else
            {
                customListView16.Clear();

                button1.BackColor = Color.LightGray;
                button1.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region listview16 (Oceny -> Usuń -> Wybierz ocenę do zamiany)
        //----------------------------------------------------------------

        private void customListView16_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView16.SelectedItems.Count > 0)
            {
                int index = customListView16.SelectedItems[0].Index;
                customListView16.gradeID = gradeDictionary2[index];

                if (comboBox2.SelectedItem != null)
                {
                    button1.BackColor = Color.Gold;
                    button1.Enabled = true;
                }
            }
            else
            {
                button1.BackColor = Color.LightGray;
                button1.Enabled = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Comboboxy i buttony
        //----------------------------------------------------------------

        #region Comboboxy
        //----------------------------------------------------------------

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (customListView9.SelectedItems.Count > 0)
            {
                button6.BackColor = Color.Lime;
                button6.Enabled = true;
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (customListView16.SelectedItems.Count > 0)
            {
                button1.BackColor = Color.Gold;
                button1.Enabled = true;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Przedmioty
        //----------------------------------------------------------------

        //Dodaj przedmiot
        private void button2_Click(object sender, EventArgs e)
        {
            DodajPrzedmiot newForm = new DodajPrzedmiot(userLogin);
            newForm.ShowDialog();
            newForm.Dispose();

            if (newForm.newSubjectAdded)                // Gdy dodano nowy przedmiot
                refillAllListViews();
        }

        // Edytuj przedmiot
        private void button4_Click(object sender, EventArgs e)
        {
            string subjectName = customListView5.SelectedItems[0].Text;

            EdytujPrzedmioty newForm = new EdytujPrzedmioty(subjectName);
            newForm.ShowDialog();
            newForm.Dispose();

            if (newForm.subjectEdited)                  // Gdy edytowano dane przedmiotu
                refillAllListViews();
        }

        // Usuń przedmiot
        private void button5_Click(object sender, EventArgs e)
        {
            string subjectName = customListView5.SelectedItems[0].Text;

            DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Potwierdź decyzję", "Czy na pewno chcesz usunąć przedmiot " + subjectName + " z bazy?", this);

            if (result == DialogResult.Yes)
            {
                formController.removeSubject(subjectName);
                refillAllListViews();
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Projekty
        //----------------------------------------------------------------

        // Dodaj projekt
        private void button8_Click(object sender, EventArgs e)
        {
            string subjectName = customListView5.SelectedItems[0].Text;

            DodajProjekt newForm = new DodajProjekt(subjectName);
            newForm.ShowDialog();
            newForm.Dispose();

            if (newForm.newProjectAdded)                  // Gdy dodano nowy projekt
                refillAllListViews();
        }

        // Edytuj projekt
        private void button9_Click(object sender, EventArgs e)
        {
            string projectName = customListView6.SelectedItems[0].Text;

            EdytujProjekt newForm = new EdytujProjekt(projectName);
            newForm.ShowDialog();
            newForm.Dispose();

            if (newForm.projectEdited)                  // Gdy edytowano dane projektu
                refillAllListViews();
        }

        // Usuń projekt
        private void button11_Click(object sender, EventArgs e)
        {
            string projectName = customListView6.SelectedItems[0].Text;

            DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Potwierdź decyzję", "Czy na pewno chcesz usunąć projekt " + projectName + " z bazy?", this);

            if (result == DialogResult.Yes)
            {
                formController.removeProject(projectName);
                refillAllListViews();
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Użytkownicy
        //----------------------------------------------------------------

        // Usuń studenta
        private void button12_Click(object sender, EventArgs e)
        {
            string subjectName = customListView5.SelectedItems[0].Text;
            string projectName = "";

            if (customListView6.SelectedItems.Count > 0)
                projectName = customListView6.SelectedItems[0].Text;

            string studentIndexNumber = customListView7.SelectedItems[0].Text;

            DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Potwierdź decyzję", "Czy na pewno chcesz usunąć studenta o nr indeksu: "
                + studentIndexNumber + " z bazy?", this);

            if (result == DialogResult.Yes)
            {
                formController.removeStudent(subjectName, projectName, studentIndexNumber);
                refillAllListViews();
            }
        }

        // Wyszukiwanie użytkownika
        private void button13_Click(object sender, EventArgs e)
        {
            string loginFragment = textBox1.Text;

            if (loginFragment != null)
            {
                List<UżytkownikDTO> usersList = formController.getUser(loginFragment);

                customListView4.fill<UżytkownikDTO>(usersList);
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Oceny
        //----------------------------------------------------------------

        // Dodaj ocenę
        private void button6_Click(object sender, EventArgs e)
        {
            string studentLogin = customListView9.SelectedItems[0].SubItems[1].Text;
            string subjectName = customListView8.SelectedItems[0].Text;
            string projectName = null;

            if (customListView10.SelectedItems.Count > 0)
                projectName = customListView10.SelectedItems[0].Text;

            OcenaDetailsDTO grade = new OcenaDetailsDTO
            {
                wartość = Double.Parse(comboBox1.Text),
                komentarz = textBox5.Text,
                nazwaProjektu = projectName,
                nazwaPrzedmiotu = subjectName,
            };

            formController.addGrade(studentLogin, grade);
            MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Ocena została dodana pomyślnie");

            refillGradeListViews();
        }

        // Usuń ocenę
        private void button7_Click(object sender, EventArgs e)
        {
            int index = customListView13.SelectedItems[0].Index;
            long gradeID = gradeDictionary[index];

            DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Potwierdź decyzję", "Czy na pewno chcesz usunąć wybraną ocenę?", this);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                formController.removeGrade(gradeID);

                MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Ocena została usunięta pomyślnie");

                refillGradeListViews();
            }
        }

        // Modyfikuj ocenę
        private void button1_Click(object sender, EventArgs e)
        {
            double newValue = Double.Parse(comboBox2.Text);
            string newDesc = textBox3.Text;

            int index = customListView16.SelectedItems[0].Index;
            long gradeID = gradeDictionary2[index];

            formController.modifyGrade(gradeID, newValue, newDesc);
            MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Ocena została zmodyfikowana pomyślnie");

            refillGradeListViews();
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Metody pomocnicze
        //----------------------------------------------------------------

        /// <summary>
        /// Czyści wszystkie listView'y, wypełnia na nowo listy przedmiotów i resetuje ustawienia przycisków na formularzu
        /// </summary>
        private void refillAllListViews()
        {
            // Wyczyszczenie wszystkich listView'ów
            foreach (var listView in formController.GetAllControlsRecursive<customListView>(this))
                listView.Clear();

            List<PrzedmiotDTO> subjectsList = formController.getSubjects();
            List<PrzedmiotProwadzącegoDTO> mySubjectsList = formController.getMySubjects();

            // Wypełnienie list przedmiotów
            customListView1.fill<PrzedmiotDTO>(subjectsList);
            customListView5.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView8.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView11.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView14.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);
            customListView17.fill<PrzedmiotProwadzącegoDTO>(mySubjectsList);

            button1.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button1.BackColor = Color.LightGray;
            button6.BackColor = Color.LightGray;
            button7.BackColor = Color.LightGray;
        }

        /// <summary>
        /// Odświeża wszystkie listView'y z ocenami i resetuje ustawienia przycisków modyfikowania i usuwania ocen
        /// </summary>
        private void refillGradeListViews()
        {
            string subjectName = "", studentLogin = "";
            List<OcenaDTO> gradesList;

            customListView13.Clear();
            customListView16.Clear();

            if (customListView12.SelectedItems.Count > 0)
            {
                subjectName = customListView11.SelectedItems[0].Text;
                studentLogin = customListView12.SelectedItems[0].SubItems[1].Text;

                gradesList = formController.getGradesFromSubject(studentLogin, subjectName);
                customListView13.fill<OcenaDTO>(gradesList);

                gradeDictionary.Clear();
                for (int i = 0; i < gradesList.Count; i++)
                    gradeDictionary.Add(i, gradesList[i].ocenaID);
            }

            if (customListView15.SelectedItems.Count > 0)
            {
                subjectName = customListView14.SelectedItems[0].Text;
                studentLogin = customListView15.SelectedItems[0].SubItems[1].Text;

                gradesList = formController.getGradesFromSubject(studentLogin, subjectName);
                customListView16.fill<OcenaDTO>(gradesList);

                gradeDictionary2.Clear();
                for (int i = 0; i < gradesList.Count; i++)
                    gradeDictionary2.Add(i, gradesList[i].ocenaID);
            }

            button1.Enabled = false;
            button7.Enabled = false;
            button1.BackColor = Color.LightGray;
            button7.BackColor = Color.LightGray;
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
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Teacher);
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
            contextMenuStrip1.Dispose();

            formController.disposeContext();
        }



        private void customListView17_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void customListView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //----------------------------------------------------------------
        #endregion

        #region Raport
        private void button10_Click(object sender, EventArgs e)
        {
            if (customListView17.SelectedItems.Count > 0)
            {
                string subjectName = customListView17.SelectedItems[0].Text;

                string koniec = formController.getSubjectInfo(subjectName) + "\r\n" + "\r\n" +
                    formController.getZestawienieOcen(subjectName) + "\r\n" + "\r\n" +
                    formController.getZestawienieStudenciProjekty(subjectName) + "\r\n" + "\r\n"+
                    formController.getNdst(subjectName);

                File.WriteAllText("Raport.txt", koniec);
            }
        }
        #endregion

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
}
