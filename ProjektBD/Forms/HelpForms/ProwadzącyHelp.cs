using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektBD.Forms.HelpForms
{
    public partial class ProwadzącyHelp : Form
    {
        public ProwadzącyHelp()
        {
            InitializeComponent();

            label3.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
