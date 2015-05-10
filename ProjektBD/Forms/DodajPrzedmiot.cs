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
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox3.Visible = true;
                label3.Visible = true;
            }
            else 
            {
                textBox3.Visible = false;
                label3.Visible = false;

                
            }
            
        }
    }
}
