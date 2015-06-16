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
        #region Pola i konstruktor
        //----------------------------------------------------------------

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

        //----------------------------------------------------------------
        #endregion

        #region Ładowanie formularza
        //----------------------------------------------------------------

        private void Komunikator_Load(object sender, EventArgs e)
        {
            refreshContactsList();
            checkForNewMessages();
        }

        //----------------------------------------------------------------
        #endregion

        #region Obsługa kontaktów
        //----------------------------------------------------------------

        // Dodaj kontakt
        private void button2_Click(object sender, EventArgs e)
        {
            AddContact form = new AddContact(formController, userLogin);
            form.ShowDialog();
            form.Dispose();

            if (form.isContactAdded)
            {
                refreshContactsList();
            }
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

            panel1.Controls.Clear();
            contactsDictionary.Clear();

            for (int i = 0; i < contactsList.Count; i++)
                contactsDictionary.Add(i, contactsList[i].RozmowaID);
        }

        //----------------------------------------------------------------
        #endregion

        #region Wysyłanie wiadomości
        //----------------------------------------------------------------

        // Wyślij wiadomość
        private void button1_Click(object sender, EventArgs e)
        {
            sendMessage();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter bez Shift'a, gdy wiadomość ma chociaż 1 znak
            if (richTextBox1.TextLength > 0 && e.KeyValue == 13 && e.Shift == false)
            {
                sendMessage();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Wysyła wiadomość wpisaną w polu richTextBox
        /// </summary>
        private void sendMessage()
        {
            if (customListView1.SelectedItems.Count > 0 && richTextBox1.TextLength > 0)
            {
                int selectedContactIndex = customListView1.SelectedItems[0].Index;
                int conversationID = contactsDictionary[selectedContactIndex];
                string msgContents = richTextBox1.Text;
                DateTime sendDate = DateTime.Now;

                formController.sendMessage(userLogin, sendDate, msgContents, conversationID);

                drawMessageOnScreen( new MessageControl(userLogin, sendDate, msgContents) );                
            }
        }

        /// <summary>
        /// Rysuje podaną wiadomość na dole panelu, po czym przesuwa scrollbar na sam dół, by była ona widoczna
        /// </summary>
        private void drawMessageOnScreen(MessageControl msg)
        {
            int controlPositionY = 0;

            foreach (Control c in panel1.Controls)
                controlPositionY += c.Height;

            // Z bliżej nieokreślonych przyczyn jeśli scroll nie jest na samej górze, coś mu się dzieje z lokalnymi współrzędnymi,
            // dlatego trzeba wziąć poprawkę na jego pozycję
            msg.Location = new Point(0, panel1.AutoScrollPosition.Y + controlPositionY);

            richTextBox1.Clear();
            panel1.Controls.Add(msg);

            /// Odświeżenie pozycji scrollbar'a
            if (panel1.VerticalScroll.Visible)
            {
                int visibleAreaY = panel1.ClientSize.Height;                // Wysokość widzialnego okna w panelu
                int activeAreaY = panel1.DisplayRectangle.Height;           // Wysokość aktywnego okna w panelu

                panel1.VerticalScroll.Value = activeAreaY - visibleAreaY;
                panel1.PerformLayout();                                     // Odświeża pozycję scrollbar'a
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Odbieranie i odświeżanie wiadomości
        //----------------------------------------------------------------

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkForNewMessages();
        }

        /// <summary>
        /// Sprawdza, czy w bazie znajdują się nowe, nieprzeczytane wiadomości i informuje o tym,
        /// pogrubiając stosowne kontakty na liście lub odświeżając okno rozmowy
        /// </summary>
        private void checkForNewMessages()
        {
            // Lista rozmów, w których istnieją nowe wiadomości
            List<int> conversationsList = formController.findConversationsWithNewMessages(userLogin);

            if (conversationsList.Count > 0)
            {
                int index;

                // Dla każdej rozmowy z nowymi wiadomościami aktualizujemy powiadomienie na liście kontaktów lub rysujemy nową wiadomość
                foreach (int convID in conversationsList)
                {
                    index = contactsDictionary.FirstOrDefault( k => k.Value == convID).Key;

                    // Jeśli wiadomość pochodzi z aktualnej rozmowy
                    if ( customListView1.SelectedItems.Count > 0 && index == customListView1.SelectedItems[0].Index )
                    {
                        List<Wiadomość> messageList = formController.getNewMessages(convID, userLogin);

                        foreach (Wiadomość msg in messageList)
                        {
                            MessageControl ms = new MessageControl(msg.nadawca, msg.dataWysłania, msg.treść);
                            ms.BackColor = Color.AntiqueWhite;
                            drawMessageOnScreen(ms);

                            msg.przeczytana = true;
                            formController.saveContext();
                        }
                    }
                    // Jeśli nie = rysuje powiadomienie (pogrubiony kontakt)
                    else
                        customListView1.Items[index].Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                }
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Wybór rozmowy
        //----------------------------------------------------------------

        private void customListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            panel1.Controls.Clear();

            if (customListView1.SelectedItems.Count > 0)
            {
                int index = customListView1.SelectedItems[0].Index;
                int conversationID = contactsDictionary[index];

                customListView1.conversationID = conversationID;

                addMessagesToPanel(conversationID);

                // Oznaczenie rozmowy jako przeczytanej
                if (customListView1.SelectedItems[0].Font.Bold == true)
                {
                    customListView1.SelectedItems[0].Font = new Font("Microsoft Sans Serif", 9);

                    formController.setConversationAsReaded(conversationID, userLogin);
                }
            }
        }

        /// <summary>
        /// Dodaje do panelu wiadomości z rozmowy podanej przez parametr
        /// </summary>
        private void addMessagesToPanel(int conversationID)
        {
            int currentHeight = 0;
            List<Wiadomość> msgList = formController.getMessages(conversationID);

            // Dodawanie wiadomości do panelu
            foreach (Wiadomość msg in msgList)
            {
                MessageControl ms = new MessageControl(msg.nadawca, msg.dataWysłania, msg.treść);

                if (msg.nadawca != userLogin)
                    ms.BackColor = Color.AntiqueWhite;

                ms.Location = new Point(0, currentHeight);
                panel1.Controls.Add(ms);

                currentHeight += ms.Size.Height;                // Do currentHeight sumuje w sobie wysokości wszystkich wiadomości,
            }                                                   // dzięki czemu te kilkulinijkowe nie zbugują wyświetlania
        }

        //----------------------------------------------------------------
        #endregion

        #region Zamykanie formularza
        //----------------------------------------------------------------

        private void Komunikator_FormClosed(object sender, FormClosedEventArgs e)
        {
            formController.disposeContext();
        }

        //----------------------------------------------------------------
        #endregion
    }
}