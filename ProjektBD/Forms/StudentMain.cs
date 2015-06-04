using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string userLogin;
        private bool close = false;

        /// <summary>
        /// Warstwa pośrednicząca między widokiem a modelem (bazą danych). Przetwarza i oblicza
        /// </summary>
        private StudentController formController;

        public StudentMain(string inputLogin)
        {
            InitializeComponent();
            userLogin = inputLogin;
            formController = new StudentController(inputLogin);
        }

        //----------------------------------------------------------------
        #endregion

        #region Ładowanie formularza

        private void StudentMain_Load(object sender, EventArgs e)
        {
            label8.Text = userLogin;

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

                customListView1.SelectedItems[0].BackColor = customListView1.previouslySelectedItemColor;
            }
        }

        private void customListView1_Leave(object sender, EventArgs e)
        {
            customListView1.saveItemState();
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
                List<OcenaDTO> mySubjectGradesList = formController.getGradesFromSubject(subjectName);

                customListView5.fill<ProjektDTO>(myProjectsList);
                customListView8.fill<OcenaDTO>(mySubjectGradesList);

                button6.Enabled = true;
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
                List<OcenaDTO> mySubjectGradesList = formController.getGradesFromSubject(selectedSubjectName);

                customListView5.fill<ProjektDTO>(myProjectsList);
                customListView8.fill<OcenaDTO>(mySubjectGradesList);

                customListView2.SelectedItems[0].BackColor = customListView2.previouslySelectedItemColor;

                button6.Enabled = true;
            }
        }

        private void customListView2_Leave(object sender, EventArgs e)
        {
            customListView2.saveItemState();
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

            customListView3.loadItemState();
        }

        private void customListView3_Leave(object sender, EventArgs e)
        {
            customListView3.saveItemState();
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

        private void customListView4_Enter(object sender, EventArgs e)
        {
            customListView4.loadItemState();
        }

        private void customListView4_Leave(object sender, EventArgs e)
        {
            customListView4.saveItemState();
        }
        //---------------------
        #endregion

        #region listView5 (Moje przedmioty i projekty -> Wybierz projekt)
        //---------------------

        private void customListView5_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView5.SelectedItems.Count > 0)
            {
                List<OcenaZProjektuDTO> myProjectGradesList = formController.getGradesFromProject(e.Item.Text);

                customListView8.fill<OcenaZProjektuDTO>(myProjectGradesList);

                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;

                List<OcenaDTO> mySubjectGradesList = formController.getGradesFromSubject( customListView2.SelectedItems[0].Text );

                customListView8.fill<OcenaDTO>(mySubjectGradesList);
            }
        }

        private void customListView5_Enter(object sender, EventArgs e)
        {
            customListView5.loadItemState();
        }

        private void customListView5_Leave(object sender, EventArgs e)
        {
            customListView5.saveItemState();
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

        private void customListView6_Enter(object sender, EventArgs e)
        {
            customListView6.loadItemState();
        }

        private void customListView6_Leave(object sender, EventArgs e)
        {
            customListView6.saveItemState();
        }

        //---------------------
        #endregion

        //----------------------------------------------------------------
        #endregion

        #region Buttony
        //----------------------------------------------------------------

        private void button8_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem(userLogin);
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

            formController.RemoveFromSubject(subjectName);

            customListView2.fill<PrzedmiotDTO>( formController.getMySubjects() );           // refresh
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

            formController.RemoveFromProject(projectName);

            customListView5.fill<ProjektDTO>( formController.getMyProjects(subjectName) );        // refresh
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

            if (this.DialogResult == DialogResult.Cancel)
            {
                DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Wyjdź", "Czy na pewno chcesz się wylogować?", this);

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Zamknięcie formatki - Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        /// </summary>
        private void StudentMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            formController.disposeContext();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Student);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem(userLogin);
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

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.About);
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            HelpFormStrategy.chooseHelpFormStrategy(HelpFormTypes.Student);
        }

        //----------------------------------------------------------------
        #endregion
    }
}