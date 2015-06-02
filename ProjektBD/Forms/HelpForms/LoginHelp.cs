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
    /// <summary>
    /// Formatka z podpowiedziami do okna logowania.
    /// </summary>
    public partial class LoginHelp : Form
    {
        public LoginHelp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zamknięcie formatki na przycisku.
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
