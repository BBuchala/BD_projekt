using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektBD.Custom_Controls
{
    public partial class MessageControl : UserControl
    {
        public MessageControl(string nadawca, DateTime dataWysłania, string treść)
        {
            InitializeComponent();

            label2.Text = nadawca;
            label4.Text = dataWysłania.ToString();
            label5.Text = treść;
        }
    }
}
