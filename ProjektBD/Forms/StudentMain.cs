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
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Forms
{
    public partial class StudentMain : Form
    {
        #region Pola i konstruktor

        /// <summary>
        /// Login zalogowanego użytkownika, można używać do wyszukiwania.
        /// </summary>
        private string userLogin;

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

        #endregion

        #region Ładowanie kart i formularza

        private void StudentMain_Load(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(pictureBox2, "Wyloguj");

            List<PrzedmiotDTO> subjectsList = formController.getSubjects();
            List<PrzedmiotDTO> mySubjectsList = formController.getMySubjects();

            customListView1.fill<PrzedmiotDTO>(subjectsList);
            customListView3.fill<PrzedmiotDTO>(subjectsList);
            customListView2.fill<PrzedmiotDTO>(mySubjectsList);
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button6.Enabled = false;
        }

        #endregion

        #region Obsługa customListView'ów
        //----------------------------------------------------------------
        // TODO: bug przy ponownym kliknięciu tego samego przedmiotu

        private void customListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string subjectName = e.Item.Text;

            List<ProjektDTO> projectsList = formController.getProjects(subjectName);
            List<StudentDTO> studentsList = formController.getStudentsFromSubject(subjectName);

            customListView4.fill<ProjektDTO>(projectsList);
            customListView7.fill<StudentDTO>(studentsList);
        }

        private void customListView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<ProjektDTO> myProjectsList = formController.getMyProjects(e.Item.Text);
            List<OcenaDTO> mySubjectGradesList = formController.getGradesFromSubject(e.Item.Text);

            customListView5.fill<ProjektDTO>(myProjectsList);
            customListView8.fill<OcenaDTO>(mySubjectGradesList);

            button6.Enabled = true;
        }

        private void customListView3_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<ProjektDTO> projectsList = formController.getNotMyProjects(e.Item.Text);

            customListView6.fill<ProjektDTO>(projectsList);

            button1.Enabled = true;
        }

        private void customListView4_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<StudentDTO> studentsList = formController.getStudentsFromProject(e.Item.Text);

            customListView7.fill<StudentDTO>(studentsList);
        }

        private void customListView5_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<OcenaDTO> myProjectGradesList = formController.getGradesFromProject(e.Item.Text);

            customListView8.fill<OcenaDTO>(myProjectGradesList);

            button3.Enabled = true;
        }

        private void customListView6_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button2.Enabled = true;
        }

        //----------------------------------------------------------------
        #endregion

        #region Buttony
        //----------------------------------------------------------------

        private void button8_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem(userLogin);
            newForm.ShowDialog();
            newForm.Dispose();
        }

        // Zapisanie na przedmiot
        private void button1_Click(object sender, EventArgs e)
        {
            string subjectName = customListView3.SelectedItems[0].Text;

            bool isNewSubject = formController.enrollToSubject(subjectName);

            if ( isNewSubject )
                MsgBoxUtils.displayInformationMsgBox("Informacja", "Zgłoszenie zostało wysłane do prowadzącego przedmiot");
            else
                MsgBoxUtils.displayInformationMsgBox("Informacja", "Jesteś już zapisany na wybrany przedmiot");
        }

        // Wypisanie z przedmiotu
        private void button6_Click(object sender, EventArgs e)
        {
            string subjectName = customListView2.SelectedItems[0].Text;

            formController.RemoveFromSubject(subjectName);

            customListView2.fill<PrzedmiotDTO>( formController.getMySubjects() );                 // refresh
        }

        // Zapisanie na projekt
        private void button2_Click(object sender, EventArgs e)
        {
            string projectName = customListView6.SelectedItems[0].Text;

            formController.enrollToProject(projectName);

            MsgBoxUtils.displayInformationMsgBox("Informacja", "Zgłoszenie zostało wysłane do prowadzącego przedmiot");
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
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
}