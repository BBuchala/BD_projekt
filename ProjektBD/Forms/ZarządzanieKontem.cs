using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using ProjektBD.DAL;
using ProjektBD.Model;
using ProjektBD.Exceptions;
using ProjektBD;
using ProjektBD.Utilities;
using ProjektBD.Controllers;
using System.Data.Entity.Core;

namespace ProjektBD.Forms
{
    public partial class Zarządzanie_Kontem : Form
    {

        #region Fields
        private string login;

        private ManageController formcontroller;
        #endregion 
        public Zarządzanie_Kontem(string inputLogin)
        {
            InitializeComponent();
            login = inputLogin;
            formcontroller = new ManageController(login, textBox4, textBox5, email, textBox2, dateTimePicker1); 
            
            
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {


           
            bool czyPoprawne =  formcontroller.validateInput1();
            
            if (czyPoprawne == true)
            {
                MsgBoxUtils.displayInformationMsgBox("Complete", "Dane zostały poprawnie zmienione.");
                email.Text = null;
                textBox2.Text = null;
            }
            else
            {
                MsgBoxUtils.displayInformationMsgBox("Błąd","Niepoprawny email");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
           bool czyPoprawne2 = formcontroller.validateInput2();
        }
    }
}
