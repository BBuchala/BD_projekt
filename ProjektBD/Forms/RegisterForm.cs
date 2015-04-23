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
using ProjektBD.Model;
using System.Data.Entity;
using ProjektBD.Exceptions;
using ProjektBD;

namespace ProjektBD.Forms
{
    /// <summary>
    /// Formatka służąca do stworzenia nowego konta. Konto studenta jest tworzone automatycznie, prowadzący musi być zaakceptowany przez admina.
    /// </summary>
    public partial class RegisterForm : Form
    {
        #region Fields

        /// <summary>
        /// Kontekst bazy danych
        /// </summary>
        private ProjektBDContext context;

        /// <summary>
        /// Lista z textBoxami, aby łatwiej je edytować (np. forEachem).
        /// </summary>
        List<TextBoxBase> textFields = new List<TextBoxBase>();

        /// <summary>
        /// Lista z labelami, aby łatwiej je edytować (np. forEachem).
        /// </summary>
        List<Label> labels = new List<Label>();

        #endregion

        #region Constructors

        public RegisterForm()
        {
            InitializeComponent();
            context = new ProjektBDContext();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Uzupełnia listy z labelami i TextBoxami. Kolejność jest ważna!
        /// Labele muszą być dodawane w tej samej kolejności co TextBoxy.
        /// Dlaczego - patrz Button_Click.
        /// </summary>
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

        /// <summary>
        /// Funkcja do wyświetlania MsgBoxa z warningiem.
        /// </summary>
        /// <param name="title">Tytuł okienka MessageBoxa.</param>
        /// <param name="text">Treść MessageBoxa.</param>
        private void displayMsgBox(string title, string text)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    
        /// <summary> 
        /// Funkcja tylko do łączenia się z bazą i dająca wybór: spróbuj ponownie
        /// lub zamknij formatkę. Utworzona, ponieważ wywoływanie rekurencyjne
        /// Form_Load to zły pomysł.
        /// To samo co w przypadku LoginForm. Rozważam utworzenie jakiegoś pliku ze
        /// wspólnymi funkcjami, nie tylko dla tych dwóch formatek.
        /// </summary>      
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

        /// <summary>
        /// Funkcja do sprawdzania powtórzeń (czy zajęty). Context Loadujemy 2 razy, bo nick
        /// i adres email trzeba sprawdzić ze wszytskimi userami, indeks tylko ze studentami.
        /// </summary>
        /// <param name="student">Jeżeli user jest studentem, dodatkowo sprawdzamy indeks.</param>
        /// <returns>Zwraca nazwę powtarzającego się atrybutu lub "" jeżeli jest ok (nic się nie powtarza).</returns>
        private string isOccupied(bool student)
        {
            String inputLogin = textFields[0].Text;
            String inputEmail = textFields[3].Text;

            context.Użytkownicy.Load();

            var query = context.Użytkownicy.Where(s => (s.email.Equals(inputEmail)));

            if (query.Count() > 0)
                return "Email";

            query = context.Użytkownicy.Where(s => (s.login.Equals(inputLogin)));

            if (query.Count() > 0)
                return "login";

            if (student)
            {
                context.Studenci.Load();                // sprawdzić - załadowanie uzytkowników powinno załadować też studentów

                query = context.Studenci.Where(s => (s.nrIndeksu == Int32.Parse(index.Text)));

                if (query.Count() > 0)
                    return "nr indeksu";
            }

            return "";
        }

        /// <summary>
        /// Metoda bazodanowa - stworzenie studenta i dodanie go do bazy.
        /// </summary>
        private void createAccountStudent()
        {
            Student s = new Student
            {
                login = textFields[0].Text,
                hasło = textFields[1].Text,
                email = textFields[3].Text,
                miejsceZamieszkania = textFields[5].Text,
                nrIndeksu = Int32.Parse(index.Text)
            };

            if (birthDate.Checked == true)
                s.dataUrodzenia = dateTimePicker1.Value;
            else s.dataUrodzenia = null;

            context.Studenci.Add(s);
            context.SaveChanges();
        }

        /// <summary>
        /// Posłanie wiadomości do admina. On akceptuje - stworzony zostaje prowadzący.
        /// </summary>
        private void notifyAdmin()
        {
            Prowadzący p = new Prowadzący
            {
                login = textFields[0].Text,
                hasło = textFields[1].Text,
                email = textFields[3].Text,
                miejsceZamieszkania = textFields[5].Text
            };

            if (birthDate.Checked == true)
                p.dataUrodzenia = dateTimePicker1.Value;
            else p.dataUrodzenia = null;


            //TO DO: posłać wiadomość do admina.
        }

        #endregion

        #region Events

        /// <summary>
        /// Metoda ładująca formatkę. Ustawiamy sposób maskowania MaskedBoxa oraz domyślny item w comboBoxie.
        /// </summary>
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            connectToDB();

            // Do pola z indeksem można wpisac tylko 6-cyfrowego inta
            index.Mask = "000000";
            index.ValidatingType = typeof(int);

            comboBox1.SelectedIndex = 1;
            index.Enabled = false;

            fillLists();
        }

        /// <summary>
        /// Uzależniamy dateTimePicker od checkBoxa.
        /// </summary>
        private void birthDate_CheckedChanged(object sender, EventArgs e)
        {
            if (birthDate.Checked == true)
                dateTimePicker1.Enabled = true;
            else dateTimePicker1.Enabled = false;
        }

        /// <summary>
        /// Metoda chowająca i disable'ująca nr indeksu (na wszelki wypadek) przy wyborze "Prowadzącego".
        /// </summary>
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                index.Visible = false;
                index.Enabled = false;
                label6.Visible = false;
                this.Height -= 50;
            }
            else
            {
                index.Visible = true;
                index.Enabled = true;
                label6.Visible = true;
                this.Height += 50;
            }
        }

        /// <summary>
        /// Metoda kolorująca button.
        /// </summary>
        private void createButton_MouseEnter(object sender, EventArgs e)
        {
            createButton.BackColor = Color.PaleTurquoise;
            createButton.ForeColor = Color.Black;
        }

        /// <summary>
        /// Metoda kolorująca button.
        /// </summary>
        private void createButton_MouseLeave(object sender, EventArgs e)
        {
            createButton.BackColor = Color.SpringGreen;
            createButton.ForeColor = Color.White;
        }

        /// <summary>
        /// Najważniejsza funkcja - sprawdza szereg warunków przed dodaniem studenta do bazy. 
        /// </summary>
        private void createButton_Click(object sender, EventArgs e)
        {

            // Wszystkie napisy znów na czarno
            foreach (Label lb in labels)
                lb.ForeColor = Color.Black;

            try
            {
                // count - 2, bo adres nieobowiązkowy, indeks sprawdzamy osobno
                for (int i = 0; i < textFields.Count - 2; i++)
                {
                    if ((textFields[i].Text.Equals("")) || (textFields[i].Text.Length < 3) || (textFields[i].Text == null))
                        throw new EmptyFieldException(i);
                }

                // Indeks sprawdzamy osobno, bo tylko w niektórych przypadkach
                if (index.Enabled)
                {
                    if ((index.Text.Equals("")) || (index.Text.Length < 3) || (index.Text == null))
                        throw new EmptyFieldException(4);

                    // Czy wszystkie pola w maskedTextBox indeksu są uzupełnione
                    if (!index.MaskCompleted)
                    {
                        displayMsgBox("Zły nr indeksu", "Podany numer indeksu jest niepełny!");
                        labels[4].ForeColor = Color.Red;
                        return;
                    }
                }

                // jeżeli hasła są różne
                if (textFields[1].Text != textFields[2].Text)
                {
                    displayMsgBox("Sprzeczność", "Podane hasła się nie zgadzają!");
                    labels[1].ForeColor = Color.Red;
                    labels[2].ForeColor = Color.Red;
                    textFields[1].Text = "";
                    textFields[2].Text = "";
                    return;
                }

                // Sprawdźmy, czy email jest dobrze podany
                if (!(SpellCheckUtilities.isValidEmail(textFields[3].Text)))
                {
                    displayMsgBox("Zły adres e-mail", "Podany adres e-mail nie jest poprawny!");
                    labels[3].ForeColor = Color.Red;
                    return;
                }

                // Czy login/email/indeks się nie pokrywa z istniejącym użytkownikiem
                string overlappingAttribute = isOccupied(index.Enabled);

                if (!overlappingAttribute.Equals(""))
                    throw new UsersOverlappingException(overlappingAttribute);
                else
                {
                    if (index.Enabled)
                    {
                        MessageBox.Show("Konto zostało poprawnie założone. Możesz się zalogować.", "Koniec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        createAccountStudent();
                    }
                    else
                    {
                        MessageBox.Show("Konto zostało poprawnie założone. Należy zaczekać na akceptację administratora.", "Koniec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        notifyAdmin();
                    }
                    this.Close();
                }

            }
            catch (EmptyFieldException err)
            {
                displayMsgBox("Brak danych", "Pola muszą zawierać co najmniej 3 znaki.");
                labels[err.getFieldNumber()].ForeColor = Color.Red;

            }
            catch (UsersOverlappingException err)
            {
                MessageBox.Show("Następujący " + err.getMessage() + " jest już zajęty.", "Złe dane", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Zamykanie formatki - messageBox z zapytaniem.
        /// </summary>
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

        /// <summary>
        /// Zaknięcie formatki - Pozbywa się utworzonego kontekstu przy zamykaniu formularza
        /// </summary>
        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (context != null)
                context.Dispose();              
        }

        #endregion


    }

}
