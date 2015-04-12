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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            ProwadzacyPrzedmioty mainForm = new ProwadzacyPrzedmioty();
            mainForm.ShowDialog();
            mainForm.Dispose();


            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void ProwadzacyMain_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
