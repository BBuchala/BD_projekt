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

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Hide();

        //    ProwadzacyPrzedmioty mainForm = new ProwadzacyPrzedmioty();
        //    mainForm.ShowDialog();
        //    mainForm.Dispose();

        //    this.Show();
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    this.Hide();

        //    ProwadzacyProjekty mainForm = new ProwadzacyProjekty();
        //    mainForm.ShowDialog();
        //    mainForm.Dispose();

        //    this.Show();
        //}

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;              // Zabezpiecza przed wpisywaniem własnych wartości
        }
    }
}
