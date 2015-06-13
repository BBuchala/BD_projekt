using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class DodajProjekt : Form
    {
        private string subjectName;

        public bool newProjectAdded = false;

        public DodajProjekt(string subjectName)
        {
            InitializeComponent();

            this.subjectName = subjectName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string projectName = textBox1.Text;
            string projectDesc = textBox2.Text;
            int maxLiczbaStud;

            // Błędnie podana maksymalna liczba studentów
            if ( Int32.TryParse(textBox3.Text, out maxLiczbaStud) == false || maxLiczbaStud <= 0 )
            {
                MsgBoxUtils.displayErrorMsgBox("Błąd", "Proszę podać prawidłową liczbę naturalną.");

                label3.ForeColor = Color.Red;
                return;
            }

            using ( ProjektBDContext context = new ProjektBDContext() )
            {
                var projectsList = context.Projekty.Where(p => p.nazwa.Equals(projectName));

                foreach (Projekt proj in projectsList)
                {
                    if (proj.Przedmiot.nazwa.Equals(subjectName))
                    {
                        MsgBoxUtils.displayErrorMsgBox("Błąd", "Projekt o podanej nazwie istnieje już w danym przedmiocie.");

                        label3.ForeColor = Color.Red;
                        return;
                    }
                }

                Przedmiot subj = context.Przedmioty
                    .Where(s => s.nazwa.Equals(subjectName))
                    .Single();

                Projekt pr = new Projekt { nazwa = projectName, opis = projectDesc, maxLiczbaStudentów = maxLiczbaStud, Przedmiot = subj };

                subj.Projekty.Add(pr);
                context.SaveChanges();
            }

            MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Projekt został dodany do przedmiotu.");
            newProjectAdded = true;

            this.Close();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                button1.BackColor = Color.Lime;
                button1.Enabled = true;
            }
            else
            {
                button1.BackColor = Color.LightGray;
                button1.Enabled = false;
            }
        }
    }
}
