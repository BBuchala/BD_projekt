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
    public partial class RegisterHelp : Form
    {
        public RegisterHelp()
        {
            InitializeComponent();

            label1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
