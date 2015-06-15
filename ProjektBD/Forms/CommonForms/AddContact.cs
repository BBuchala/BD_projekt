using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Controllers;
using ProjektBD.DAL;
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Forms.CommonForms
{
    public partial class AddContact : Form
    {
        /// <summary>
        /// Login zalogowanego użytkownika
        /// </summary>
        private string userLogin;

        /// <summary>
        /// Kontroler do zarządzania i komunikowania się z bazą danych.
        /// </summary>
        private MessageController formController;

        /// <summary>
        /// Określa, czy nowy kontakt został dodany
        /// </summary>
        public bool isContactAdded = false;

        internal AddContact(MessageController controller, string login)
        {
            InitializeComponent();

            customListView1.MultiSelect = true;

            formController = controller;
            userLogin = login;
        }

        // Szukaj użytkownika
        private void button1_Click(object sender, EventArgs e)
        {
            string loginFragment = textBox1.Text;

            if (loginFragment != null)
            {
                List<UżytkownikDTO> usersList = formController.getUsers(loginFragment);

                customListView1.fill<UżytkownikDTO>(usersList);
            }
        }

        // Dodaj kontakt
        private void button2_Click(object sender, EventArgs e)
        {
            string newContact = "";

            // newContact <- lista osób, które chcemy dodać do rozmowy (kontaktu)
            foreach (ListViewItem item in customListView1.SelectedItems)
            {
                newContact += item.Text + ", ";
            }
            newContact = newContact.Remove(newContact.Length - 2, 2);           // Usunięcie przecinka na końcu

            List<KontaktyDTO> currentContacts = formController.getContacts();

            // Sprawdzenie, czy nie chcemy dodać kontaktu już istniejącego
            foreach (KontaktyDTO contact in currentContacts)
            {
                if ( contact.rozmówcy.Equals(newContact) )
                {
                    MsgBoxUtils.displayErrorMsgBox("Błąd", "Podana osoba bądź grupa osób znajduje się już na liście kontaktów.");
                    return;
                }
            }

            List<Użytkownik> usersInConversation = new List<Użytkownik>();

            // Dodanie do listy użytkownika głównego
            usersInConversation.Add( formController.findUser(userLogin) );

            // Dodanie do listy użytkowników wybranych w listView'ie
            foreach (ListViewItem item in customListView1.SelectedItems)
                usersInConversation.Add(formController.findUser(item.Text));

            formController.addConversation(usersInConversation);

            MsgBoxUtils.displayInformationMsgBox("Operacja ukończona pomyślnie", "Kontakt został dodany pomyślnie");
            isContactAdded = true;

            this.Close();
        }

        private void customListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView1.SelectedItems.Count > 0)
            {
                button2.BackColor = Color.Lime;
                button2.Enabled = true;
            }
            else
            {
                button2.BackColor = Color.LightGray;
                button2.Enabled = false;
            }
        }
    }
}
