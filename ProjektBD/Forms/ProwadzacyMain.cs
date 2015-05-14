using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektBD.Forms
{
    public partial class ProwadzacyMain : Form
    {
        public ProwadzacyMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DodajPrzedmiot newForm = new DodajPrzedmiot();
            newForm.ShowDialog();
            newForm.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zarządzanie_Kontem newForm = new Zarządzanie_Kontem();
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
    }
}
