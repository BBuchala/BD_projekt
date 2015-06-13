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
using ProjektBD.Utilities;
using ProjektBD.Model;

namespace ProjektBD.Forms.TeacherForms
{
    public partial class DodajPrzedmiot : Form
    {
        private string teacherLogin;

        public bool newSubjectAdded = false;

        public DodajPrzedmiot(string teacherName)
        {
            InitializeComponent();

            this.teacherLogin = teacherName;
            this.Height -= 50;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Visible = true;
                label1.Visible = true;
                this.Height += 50;
            }
            else
            {
                textBox1.Visible = false;
                label1.Visible = false;
                this.Height -= 50;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string subjectName = textBox4.Text;
            string subjectDesc = textBox2.Text;
            int maxLiczbaStud = 0;

            // Gdy przedmiot obieralny
            if ( checkBox1.Checked && (Int32.TryParse(textBox1.Text, out maxLiczbaStud) == false || maxLiczbaStud <= 0) )
            {
                MsgBoxUtils.displayErrorMsgBox("Błąd", "Proszę podać prawidłową liczbę naturalną.");

                label1.ForeColor = Color.Red;
                return;
            }

            using ( ProjektBDContext context = new ProjektBDContext() )
            {
                Przedmiot subj = context.Przedmioty
                    .Where( p => p.nazwa.Equals(subjectName) )
                    .FirstOrDefault();

                if (subj != null)
                {
                    MsgBoxUtils.displayErrorMsgBox("Błąd", "Przedmiot o podanej nazwie istnieje już w bazie.");

                    label5.ForeColor = Color.Red;
                    return;
                }

                Prowadzący subjectSensei = context.Prowadzący
                    .Where( p => p.login.Equals(teacherLogin) )
                    .Single();

                if (checkBox1.Checked)
                    subj = new PrzedmiotObieralny
                    {
                        nazwa = subjectName,
                        opis = subjectDesc,
                        maxLiczbaStudentów = maxLiczbaStud,
                        Prowadzący = subjectSensei
                    };

                else
                    subj = new Przedmiot
                    {
                        nazwa = subjectName,
                        opis = subjectDesc,
                        Prowadzący = subjectSensei
                    };

                context.Przedmioty.Add(subj);
                context.SaveChanges();

                MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Przedmiot został dodany do bazy.");
                newSubjectAdded = true;

                this.Close();
            }
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                button2.BackColor = Color.Lime;
                button2.Enabled = true;
            }
            else
            {
                button2.BackColor = Color.LightGray;
                button2.Enabled = false;
            }
        }
    }
}
