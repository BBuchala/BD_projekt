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
    public partial class Zarządzanie_Kontem : Form
    {
        public Zarządzanie_Kontem()
        {
            InitializeComponent();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
