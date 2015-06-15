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
using ProjektBD.Custom_Controls;
using ProjektBD.Forms.CommonForms;
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Forms
{
    public partial class Komunikator : Form
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
        /// Słownik przechowujący zmapowane wartości ID rozmów i ich indeksów w kontrolce CustomListView
        /// </summary>
        private Dictionary<int, int> contactsDictionary = new Dictionary<int, int>(); 

        public Komunikator(string login)
        {
            InitializeComponent();

            this.userLogin = login;
            formController = new MessageController(userLogin);
        }

        private void Komunikator_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                MessageControl ms = new MessageControl();

                ms.Location = new Point(0, i * ms.Size.Height);
                panel1.Controls.Add(ms);
            }

            refreshContactsList();
        }

        // Dodaj kontakt
        private void button2_Click(object sender, EventArgs e)
        {
            AddContact form = new AddContact(formController, userLogin);
            form.ShowDialog();
            form.Dispose();

            if (form.isContactAdded)
                refreshContactsList();
        }

        // Usuń kontakt
        private void button3_Click(object sender, EventArgs e)
        {
            if (customListView1.SelectedItems.Count > 0)
            {
                int index = customListView1.SelectedItems[0].Index;
                int conversationID = contactsDictionary[index];

                DialogResult result = MsgBoxUtils.displayQuestionMsgBox("Potwierdź decyzję", "Czy na pewno chcesz usunąć wybrany kontakt?", this);

                if (result == DialogResult.Yes)
                {
                    formController.deleteConversation(conversationID);
                    refreshContactsList();
                }
            }
        }

        /// <summary>
        /// Odświeża listę kontaktów.
        /// </summary>
        private void refreshContactsList()
        {
            List<KontaktyDTO> contactsList = formController.getContacts();
            customListView1.fill<KontaktyDTO>(contactsList);

            contactsDictionary.Clear();
            for (int i = 0; i < contactsList.Count; i++)
                contactsDictionary.Add(i, contactsList[i].RozmowaID);
        }

        private void customListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (customListView1.SelectedItems.Count > 0)
            {
                int index = customListView1.SelectedItems[0].Index;
                customListView1.conversationID = contactsDictionary[index];
            }
        }
    }
}
