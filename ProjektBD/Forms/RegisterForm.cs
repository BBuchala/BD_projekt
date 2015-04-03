using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjektBD.DAL;
using ProjektBD.Exceptions;

namespace ProjektBD.Forms
{
    public partial class RegisterForm : Form
    {

        private ProjektBDContext context;

        // Listy, odpowiednio z textBoxami i labelami, do łatwiejszego operowania
        // TextBoxBase - nadrzędna dla textBoxa i maskedTextBoxa, więc oba idą do tej samej listy.
        List<TextBoxBase> textFields = new List<TextBoxBase>();
        List<Label> labels = new List<Label>();


        public RegisterForm()
        {
            InitializeComponent();
            context = new ProjektBDContext();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            connectToDB();
            index.Mask = "000000";
            index.ValidatingType = typeof(int);
            fillLists();
        }

        
        /*
         * Uzupełnia listy z labelami i TextBoxami. Kolejność jest ważna!
         * Labele muszą być dodawane w tej samej kolejności co TextBoxy.
         * Dlaczego - patrz Button_Click.
         */ 
        private void fillLists()
        {
            textFields.Add(login);
            textFields.Add(password1);
            textFields.Add(password2);
            textFields.Add(email);
            textFields.Add(index);
            textFields.Add(address);
           
            // label1 - tekst informujący, label 7 - ten od daty
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label8);

        }

        /*
         * Najważniejsza funkcja - sprawdza szereg warunków przed dodaniem studenta do bazy. 
         */
        private void createButton_Click(object sender, EventArgs e)
        {
            DateTime birth = dateTimePicker1.Value;

            // Wszystkie napisy znów na czarno
            foreach (Label lb in labels)
                lb.ForeColor = Color.Black;

            try
            {
                // count - 1, bo adres nieobowiązkowy
                for (int i = 0; i < textFields.Count - 1; i++) 
                {
                    if ((textFields[i].Text == "") || (textFields[i].Text == null))
                        throw new EmptyFieldException(i);
                }

                // jeżeli hasła są różne
                if (textFields[1].Text != textFields[2].Text)
                {
                    displayMsgBox("Sprzeczność", "Podane hasła się nie zgadzają!");
                    labels[1].ForeColor = Color.Red;
                    labels[2].ForeColor = Color.Red;
                    return;
                }

                // TO DO
                // Spellcheck emaila + hasła
                // Sprawdzenie bazy (login, nr indeks, email) pod kątem powtórzeń
                
            }
            catch (EmptyFieldException err)
            {
                displayMsgBox("Brak danych", "Nie wszystkie pola zostały uzupełnione.");
                labels[err.getFieldNumber()].ForeColor = Color.Red;
            }
            catch (FormatException)
            {

            }
            finally
            {
                foreach (TextBoxBase txt in textFields)
                    txt.Text = "";
            }
        }

        /*
         * Funkcja do wyświetlania MsgBoxa.
         */ 
        private void displayMsgBox(string title, string text)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void createButton_MouseEnter(object sender, EventArgs e)
        {
            createButton.BackColor = Color.PaleTurquoise;
            createButton.ForeColor = Color.Black;
        }

        private void createButton_MouseLeave(object sender, EventArgs e)
        {
            createButton.BackColor = Color.SpringGreen;
            createButton.ForeColor = Color.White;
        }


        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;

            if (this.DialogResult == DialogResult.Cancel)
            {
                switch (MessageBox.Show(this, "Jesteś pewien, że chcesz opuścić okno rejestracji?", "Wyjdź", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (context != null)
                context.Dispose();              // Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        }

        //***********************************************************************
        // Funkcja tylko do łączenia się z bazą i dająca wybór: spróbuj ponownie
        // lub zamknij formatkę. Utworzona, ponieważ wywoływanie rekurencyjne
        // Form_Load to zły pomysł.
        // To samo co w przypadku LoginForm. Rozważam utworzenie jakiegoś pliku ze
        // wspólnymi funkcjami, nie tylko dla tych dwóch formatek.
        //***********************************************************************
        private void connectToDB()
        {
            try
            {
                context.Database.Initialize(false);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                DialogResult connRetry = MessageBox.Show("Nastąpił błąd podczas próby połączenia z bazą danych.\n Upewnij się, czy nie jesteś połączony w innym miejscu. \n Spróbować ponownie?",
                                                       "Błąd połączenia",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Exclamation);
                if (connRetry == DialogResult.No)
                    this.Close();
                else
                    connectToDB();
            }
        }


    }
}
