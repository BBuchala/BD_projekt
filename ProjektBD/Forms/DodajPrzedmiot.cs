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
    public partial class DodajPrzedmiot : Form
    {
        public DodajPrzedmiot()
        {
            InitializeComponent();

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
    }
}
