using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ProjektBD.Controllers;
using ProjektBD.Custom_Controls;
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Forms
{
    public partial class StudentMain : Form
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        /// <summary>
        /// Login zalogowanego użytkownika, można używać do wyszukiwania.
        /// </summary>
        private string studentLogin;

        private bool close = false;

        /// <summary>
        /// Warstwa pośrednicząca między widokiem a modelem (bazą danych). Przetwarza i oblicza
        /// </summary>
        private StudentController formController;

        /// <summary>
        /// Słownik przechowujący zmapowane wartości ID ocen i ich indeksów w kontrolce CustomListView
        /// </summary>
        private Dictionary<int, long> gradeDictionary = new Dictionary<int,long>();          // <indeks w kontrolce, ID oceny>

        public StudentMain(string inputLogin)
        {
            InitializeComponent();

            studentLogin = inputLogin;
            formController = new StudentController(inputLogin);
        }

        //----------------------------------------------------------------
        #endregion

        #region Ładowanie formularza

        private void StudentMain_Load(object sender, EventArgs e)
        {
            label8.Text = studentLogin;

            new ToolTip().SetToolTip(pictureBox2, "Wyloguj");

            List<PrzedmiotDTO> subjectsList = formController.getSubjects();
            List<PrzedmiotDTO> mySubjectsList = formController.getMySubjects();

            customListView1.fill<PrzedmiotDTO>(subjectsList);
            customListView3.fill<PrzedmiotDTO>(subjectsList);
            customListView2.fill<PrzedmiotDTO>(mySubjectsList);
        }

        #endregion

        #region Obsługa customListView'ów
        //----------------------------------------------------------------

        #region listView1 (Podgląd -> Lista przedmiotów)
        //---------------------

        private void customListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView1.SelectedItems.Count > 0)
            {
                string subjectName = e.Item.Text;

                List<ProjektDTO> projectsList = formController.getProjects(subjectName);
                List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

                customListView4.fill<ProjektDTO>(projectsList);
                customListView7.fill<StudentDTO>(studentsList);
            }
            else
            {
                customListView4.Clear();
                customListView7.Clear();
            }
        }

        private void customListView1_Enter(object sender, EventArgs e)
        {
            if (customListView1.SelectedItems.Count > 0)
            {
                string subjectName = customListView1.SelectedItems[0].Text;

                List<ProjektDTO> projectsList = formController.getProjects(subjectName);                // do wywalenia?
                List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

                customListView4.fill<ProjektDTO>(projectsList);
                customListView7.fill<StudentDTO>(studentsList);
            }
        }

        //---------------------
        #endregion

        #region listView2 (Moje przedmioty i projekty -> Wybierz przedmiot)
        //---------------------

        private void customListView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView2.SelectedItems.Count > 0)
            {
                string subjectName = e.Item.Text;

                List<ProjektDTO> myProjectsList = formController.getMyProjects(subjectName);
                List<OcenaDTO> mySubjectGradesList = formController.getGradesFromSubject(studentLogin, subjectName);

                customListView5.fill<ProjektDTO>(myProjectsList);
                customListView8.fill<OcenaDTO>(mySubjectGradesList);

                button6.Enabled = true;

                gradeDictionary.Clear();
                for (int i = 0; i < mySubjectGradesList.Count; i++)
                    gradeDictionary.Add(i, mySubjectGradesList[i].ocenaID);
            }
            else
            {
                button6.Enabled = false;
                customListView5.Clear();
                customListView8.Clear();
            }
        }

        private void customListView2_Enter(object sender, EventArgs e)
        {
            button3.Enabled = false;

            // Gdy coś jest zaznaczone, wypełniamy pozostałe listView'y i odblokowujemy przycisk
            if (customListView2.SelectedItems.Count > 0)
            {
                string selectedSubjectName = customListView2.SelectedItems[0].Text;

                List<ProjektDTO> myProjectsList = formController.getMyProjects(selectedSubjectName);            // do wywalenia?
                List<OcenaDTO> mySubjectGradesList = formController.getGradesFromSubject(studentLogin, selectedSubjectName);

                customListView5.fill<ProjektDTO>(myProjectsList);
                customListView8.fill<OcenaDTO>(mySubjectGradesList);

                button6.Enabled = true;

                gradeDictionary.Clear();
                for (int i = 0; i < mySubjectGradesList.Count; i++)
                    gradeDictionary.Add(i, mySubjectGradesList[i].ocenaID);
            }
        }

        //---------------------
        #endregion

        #region listView3 (Zgłoszenie -> Lista przedmiotów)
        //---------------------

        private void customListView3_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView3.SelectedItems.Count > 0)
            {
                List<ForeignProjektDTO> projectsList = formController.getNotMyProjects(e.Item.Text);

                customListView6.fill<ForeignProjektDTO>(projectsList);

                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                customListView6.Clear();
            }
        }

        private void customListView3_Enter(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        //---------------------
        #endregion

        #region listView4 (Podgląd -> Lista projektów)
        //---------------------

        private void customListView4_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView4.SelectedItems.Count > 0)
            {
                List<StudentDTO> studentsList = formController.getStudentsFromProject(e.Item.Text);

                customListView7.fill<StudentDTO>(studentsList);
            }
            else
            {
                List<StudentDTO> studentsList = formController.getStudentsFromSubject( customListView1.SelectedItems[0].Text );

                customListView7.fill<StudentDTO>(studentsList);
            }
        }

        //---------------------
        #endregion

        #region listView5 (Moje przedmioty i projekty -> Wybierz projekt)
        //---------------------

        private void customListView5_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView5.SelectedItems.Count > 0)
            {
                List<OcenaZProjektuDTO> myProjectGradesList = formController.getGradesFromProject(studentLogin, e.Item.Text);

                customListView8.fill<OcenaZProjektuDTO>(myProjectGradesList);

                button3.Enabled = true;

                gradeDictionary.Clear();
                for (int i = 0; i < myProjectGradesList.Count; i++)
                    gradeDictionary.Add(i, myProjectGradesList[i].ocenaID);
            }
            else
            {
                button3.Enabled = false;

                List<OcenaDTO> mySubjectGradesList = formController.getGradesFromSubject(studentLogin, customListView2.SelectedItems[0].Text);

                customListView8.fill<OcenaDTO>(mySubjectGradesList);

                gradeDictionary.Clear();
                for (int i = 0; i < mySubjectGradesList.Count; i++)
                    gradeDictionary.Add(i, mySubjectGradesList[i].ocenaID);
            }
        }

        //---------------------
        #endregion

        #region listView6 (Zgłoszenie -> Lista projektów)
        //---------------------

        private void customListView6_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView6.SelectedItems.Count > 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        //---------------------
        #endregion

        #region listView8 (Moje przedmioty i projekty -> Twoje oceny)
        //----------------------------------------------------------------

        private void customListView8_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            customListView lv = (sender as customListView);

            if (lv.SelectedItems.Count > 0)
            {
                int index = lv.SelectedItems[0].Index;
                lv.gradeID = gradeDictionary[index];
            }
        }

        //----------------------------------------------------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Buttony
        //----------------------------------------------------------------

        // Zapisanie na przedmiot
        private void button1_Click(object sender, EventArgs e)
        {
            string subjectName = customListView3.SelectedItems[0].Text;

            switch ( formController.enrollToSubject(subjectName) )
            {
                case "Znaleziono aplikację na przedmiot":
                    MsgBoxUtils.displayInformationMsgBox("Infomacja", "Podczekaj na akceptację bądź odrzucenie aplikacji przez prowadzącego.");
                    break;

                case "Już zapisany":
                    MsgBoxUtils.displayInformationMsgBox("Infomacja", "Jesteś już zapisany na wybrany przedmiot.");
                    break;

                case "Zapisywanie zakończone pomyślnie":
                    MsgBoxUtils.displayInformationMsgBox("Informacja", "Zgłoszenie zostało wysłane do prowadzącego przedmiot.");
                    break;
            }
        }

        // Wypisanie z przedmiotu
        private void button6_Click(object sender, EventArgs e)
        {
            string subjectName = customListView2.SelectedItems[0].Text;

            formController.RemoveFromSubject(studentLogin, subjectName);

            customListView2.fill<PrzedmiotDTO>( formController.getMySubjects() );           // refresh
            customListView5.Clear();
            customListView8.Clear();
        }
        
        // Zapisanie na projekt
        private void button2_Click(object sender, EventArgs e)
        {
            string projectName = customListView6.SelectedItems[0].Text;

            switch ( formController.enrollToProject(projectName) )
            {
                case "Znaleziono aplikację na projekt":
                    MsgBoxUtils.displayInformationMsgBox("Infomacja", "Podczekaj na akceptację bądź odrzucenie aplikacji przez prowadzącego.");
                    break;

                case "Niezapisany na przedmiot nadrzędny":
                    MsgBoxUtils.displayInformationMsgBox("Informacja", "Najpierw zapisz się na przedmiot, do którego należy ten projekt.");
                    break;

                case "Zapisywanie zakończone pomyślnie":
                    MsgBoxUtils.displayInformationMsgBox("Informacja", "Zgłoszenie zostało wysłane do prowadzącego przedmiot.");
                    break;
            }
        }

        // Wypisanie z projektu
        private void button3_Click(object sender, EventArgs e)
        {
            string subjectName = customListView2.SelectedItems[0].Text;
            string projectName = customListView5.SelectedItems[0].Text;

            formController.RemoveFromProject(studentLogin, projectName);

            customListView5.fill<ProjektDTO>( formController.getMyProjects(subjectName) );        // refresh
            customListView8.Clear();
        }

        // Wyszukiwanie użytkownika
        private void button13_Click(object sender, EventArgs e)
        {
            string loginFragment = textBox1.Text;

            if (loginFragment != null)
            {
                List<UżytkownikDTO> usersList = formController.getUser(loginFragment);

                customListView9.fill<UżytkownikDTO>(usersList);
            }
        }

        // Odświeżenie listy przedmiotów
        private void button4_Click(object sender, EventArgs e)
        {
            List<PrzedmiotDTO> mySubjectsList = formController.getMySubjects();

            customListView2.fill<PrzedmiotDTO>(mySubjectsList);

            customListView5.Clear();
            customListView8.Clear();
            button3.Enabled = false;
            button6.Enabled = false;
        }

        //----------------------------------------------------------------
        #endregion

        #region Wiadomości
        //----------------------------------------------------------------

        private void messageImage_Click(object sender, EventArgs e)
        {
            openCommunicator();
        }

        private void messageCount_Click(object sender, EventArgs e)
        {
            openCommunicator();
        }

        /// <summary>
        /// Metoda otwierająca komunikator lub uaktywniająca go, gdy został już wcześniej otworzony
        /// </summary>
        private void openCommunicator()
        {
            var openedGGForms = Application.OpenForms.OfType<Komunikator>().ToList();

            // Blokuje możliwość otwarcia drugiego komunikatora
            if (openedGGForms.Count == 0)
            {
                Komunikator form = new Komunikator(studentLogin);
                form.Show();
            }
            else
            {
                if (openedGGForms[0].WindowState == FormWindowState.Minimized)
                    openedGGForms[0].WindowState = FormWindowState.Normal;
                else
                    openedGGForms[0].Activate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkForNewMessages();
        }

        /// <summary>
        /// Szuka nowych wiadomości zaadresowanych do użytkownika
        /// </summary>
        private void checkForNewMessages()
        {
            int newMessagesCount = formController.getNewMessagesCount();

            if (newMessagesCount > 0)
            {
                messageImage.Image = ProjektBD.Properties.Resources.mail;
                messageCount.Visible = true;

                if (newMessagesCount <= 100)
                    messageCount.Text = newMessagesCount.ToString();
                else
                    messageCount.Text = "99+";
            }
            else
            {
                messageImage.Image = ProjektBD.Properties.Resources.mail2;
                messageCount.Visible = false;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Help i zarządzanie kontem
        //----------------------------------------------------------------

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Student);
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.About);
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Student);
        }

        // Zarządzanie kontem
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem(studentLogin);
            newForm.ShowDialog();

            if (newForm.close == true)
            {
                newForm.Dispose();
                close = true;
                this.Close();
            }
            else
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
        /// Zamykanie formatki - messageBox z zapytaniem.
        /// </summary>
        private void StudentMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (close == true)
                return;

            DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Wyjdź", "Czy na pewno chcesz się wylogować?", this);

            if (result == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Zamknięcie formatki - Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        /// </summary>
        private void StudentMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            formController.disposeContext();
        }

        //----------------------------------------------------------------
        #endregion
    }
}