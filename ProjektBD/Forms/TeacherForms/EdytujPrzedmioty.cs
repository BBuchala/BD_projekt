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
    public partial class EdytujPrzedmioty : Form
    {
        private string subjectName;

        private ProjektBDContext context = new ProjektBDContext();

        public bool subjectEdited = false;

        public EdytujPrzedmioty(string subjectName)
        {
            InitializeComponent();

            textBox1.Text = subjectName;
            this.subjectName = subjectName;
        }

        private void EdytujPrzedmioty_Load(object sender, EventArgs e)
        {
            var subject = ( from sub in context.Przedmioty
                                join ob in context.PrzedmiotyObieralne on sub.PrzedmiotID equals ob.PrzedmiotID into obier

                            from obierki in obier.DefaultIfEmpty()
                            where sub.nazwa.Equals(subjectName)
                            select new PrzedmiotDetailsDTO
                            {
                                nazwa = sub.nazwa,
                                opis = sub.opis,
                                liczbaStudentów = sub.liczbaStudentów,
                                maxLiczbaStudentów = obierki.maxLiczbaStudentów
                            }
                            ).Single();

            textBox2.Text = subject.opis;

            if (subject.maxLiczbaStudentów.HasValue)
                textBox3.Text = subject.maxLiczbaStudentów.Value.ToString();
            else
            {
                label3.Visible = false;
                textBox3.Visible = false;

                this.Height -= 50;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxLiczbaStud = 0;
            string subjectTypeName;

            var subj = context.Przedmioty
                .Where( p => p.nazwa.Equals(subjectName) )
                .Single();

            subjectTypeName = subj.GetType().Name;

            if ( subjectTypeName.Equals("PrzedmiotObieralny") )
            {
                // Błędnie podana maksymalna liczba studentów
                if ( Int32.TryParse(textBox3.Text, out maxLiczbaStud) == false || maxLiczbaStud <= 0 )
                {
                    MsgBoxUtils.displayErrorMsgBox("Błąd", "Proszę podać prawidłową liczbę naturalną.");

                    label3.ForeColor = Color.Red;
                    return;
                }

                // Maksymalna liczba studentów mniejsza od liczby studentów
                else if (maxLiczbaStud < subj.liczbaStudentów)
                {
                    MsgBoxUtils.displayErrorMsgBox("Błąd", "Ilość osób zapisanych na przedmiot przewyższa ustalony limit miejsc.");

                    label3.ForeColor = Color.Red;
                    return;
                }
            }

            this.subjectName = textBox1.Text;                   // Aktualizacja nazwy przedmiotu, który edytujemy
            subj.nazwa = textBox1.Text;
            subj.opis = textBox2.Text;

            if ( subjectTypeName.Equals("PrzedmiotObieralny") )
                (subj as PrzedmiotObieralny).maxLiczbaStudentów = maxLiczbaStud;

            context.SaveChanges();

            MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Dane przedmiotu zostały pomyślnie zaktualizowane.");

            label3.ForeColor = SystemColors.ControlText;
            this.subjectEdited = true;
        }

        private void EdytujPrzedmioty_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Dispose();
        }
    }
}
