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
        #region Fields and constructor
        //----------------------------------------------------------------

        private string login;
        private ManageController formcontroller;
        public bool close = false;

        public Zarządzanie_Kontem(string inputLogin)
        {
            InitializeComponent();
            login = inputLogin;

            formcontroller = new ManageController(login, textBox4, textBox5, email, textBox2, dateTimePicker1, textBox3); 
        }

        //----------------------------------------------------------------
        #endregion 

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                formcontroller.deleteUser();

                MsgBoxUtils.displayInformationMsgBox("Complete", "Usunięto użytkownika z bazy");

                close = true;
                this.Close();
            }
            else
                MsgBoxUtils.displayInformationMsgBox("Error", "Należy potwierdzić wybór");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string  czyPoprawne1 =  formcontroller.validateInput1();

            switch (czyPoprawne1)
            {
                case "Zla dlugosc":
                    MsgBoxUtils.displayInformationMsgBox("Error", "Zła długość pół. Pola musza zajmować conajmniej 3 znaki");

                    email.Text = null;
                    textBox2.Text = null;
                    return;

                case "Zla forma email":
                    MsgBoxUtils.displayInformationMsgBox("Error", "Zły adres email.");

                    email.Text = null;
                    textBox2.Text = null;
                    return;

                case "ok":
                    MsgBoxUtils.displayInformationMsgBox("Complete", "Dane zostały poprawnie zmienione");

                    email.Text = null;
                    textBox2.Text = null;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string czyPoprawne2 = formcontroller.validateInput2();

            switch (czyPoprawne2)
            {
                case "Zla dlugosc":
                    MsgBoxUtils.displayInformationMsgBox("Error", "Zła długość pół. Pola musza zajmować conajmniej 3 znaki");

                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    return;

                case "Rozne hasla":
                    MsgBoxUtils.displayInformationMsgBox("Error", "Nowe hasło nie jest takie samo.");

                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    return;

                case "Zle stare haslo":
                    MsgBoxUtils.displayInformationMsgBox("Error", "Stare hasło jest niepoprawne.");

                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    return;

                case "ok":
                    MsgBoxUtils.displayInformationMsgBox("Complete", "Dane zostały poprawnie zmienione");

                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                 
                    break;
            }
        }

        private void ZarządznieKontem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            if (close == true)
                return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Zamknij ustawienia", "Jesteś pewien, że chcesz wyjść z ustawień konta?", this);

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
        private void ZarządzanieKontem_FormClosed(object sender, FormClosedEventArgs e)
        {
            formcontroller.disposeContext();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 3)
                //this.Size.Height = 450;
                this.AutoScroll = false;
            else
                this.AutoScroll = true;
        }
    }
}
