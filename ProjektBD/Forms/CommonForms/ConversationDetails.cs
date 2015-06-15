using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Model;

namespace ProjektBD.Forms.CommonForms
{
    public partial class ConversationDetails : Form
    {
        public ConversationDetails(ConversationDetailsDTO convesationDetails)
        {
            InitializeComponent();

            label3.Text = convesationDetails.rozmówcy;
            label5.Text = convesationDetails.dataRozpoczęcia.ToString();
            label7.Text = convesationDetails.ilośćWiadomości.ToString();
        }
    }
}
