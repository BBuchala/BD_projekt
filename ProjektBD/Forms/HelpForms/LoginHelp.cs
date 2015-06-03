using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Utilities;

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

            label1.Select();            // drobne oszustwo - przenosi focus na pierwszy element -> scroll idzie na samą górę
        }

        /// <summary>
        /// Zamknięcie formatki na przycisku.
        /// </summary>
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
