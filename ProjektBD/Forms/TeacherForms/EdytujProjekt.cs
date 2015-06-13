using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.DAL;
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Forms.TeacherForms
{
    public partial class EdytujProjekt : Form
    {
        private string projectName;

        private ProjektBDContext context = new ProjektBDContext();

        public bool projectEdited = false;

        public EdytujProjekt(string projectName)
        {
            InitializeComponent();

            this.projectName = projectName;
        }

        private void EdytujProjekt_Load(object sender, EventArgs e)
        {
            if (projectName != null)
            {
                Projekt proj = context.Projekty
                    .Where(p => p.nazwa.Equals(projectName))
                    .Single();

                textBox1.Text = proj.nazwa;
                textBox2.Text = proj.opis;
                textBox3.Text = proj.maxLiczbaStudentów.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string projectNewName = textBox1.Text;
            string projectDesc = textBox2.Text;
            int maxLiczbaStud;

            // Błędnie podana maksymalna liczba studentów
            if (Int32.TryParse(textBox3.Text, out maxLiczbaStud) == false || maxLiczbaStud <= 0)
            {
                MsgBoxUtils.displayErrorMsgBox("Błąd", "Proszę podać prawidłową liczbę naturalną.");

                label3.ForeColor = Color.Red;
                return;
            }

            // "Stary" projekt
            Projekt proj = context.Projekty
                .Where( p => p.nazwa.Equals(projectName) )
                .Include("Przedmiot")
                .Single();

            // Gdy zmieniono nazwę przedmiotu
            if ( projectName.Equals(projectNewName) == false )
            {
                // Lista projektów ze wszystkich przedmiotów, które nazywają się jak nowy
                var projectsList = context.Projekty.Where( p => p.nazwa.Equals(projectNewName) );

                string subjectName = proj.Przedmiot.nazwa;

                foreach (Projekt p in projectsList)
                {
                    if (p.Przedmiot.nazwa.Equals(subjectName))
                    {
                        MsgBoxUtils.displayErrorMsgBox("Błąd", "Projekt o podanej nazwie istnieje już w danym przedmiocie.");

                        label1.ForeColor = Color.Red;
                        return;
                    }
                }
            }

            proj.nazwa = projectNewName;
            proj.opis = projectDesc;
            proj.maxLiczbaStudentów = maxLiczbaStud;

            context.SaveChanges();

            MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Dane projektu zostały pomyślnie zaktualizowane.");

            label1.ForeColor = SystemColors.ControlText;
            label3.ForeColor = SystemColors.ControlText;
            this.projectEdited = true;
        }

        private void EdytujProjekt_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Dispose();
        }
    }
}
