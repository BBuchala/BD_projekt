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

        private void StudentMain_Load(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(pictureBox2, "Wyloguj");

            List<PrzedmiotDTO> subjectsList = formController.getSubjects();
            List<PrzedmiotDTO> mySubjectsList = formController.getMySubjects();

            customListView1.fill<PrzedmiotDTO>(subjectsList);
            customListView3.fill<PrzedmiotDTO>(subjectsList);
            customListView2.fill<PrzedmiotDTO>(mySubjectsList);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<ProjektDTO> projectsList = formController.getProjects(e.Item.Text);

            customListView4.fill<ProjektDTO>(projectsList);
        }

        private void customListView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<ProjektDTO> myProjectsList = formController.getMyProjects(e.Item.Text);

            customListView5.fill<ProjektDTO>(myProjectsList);
        }

        private void customListView3_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<ProjektDTO> projectsList = formController.getNotMyProjects(e.Item.Text);

            customListView6.fill<ProjektDTO>(projectsList);
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
    }
}