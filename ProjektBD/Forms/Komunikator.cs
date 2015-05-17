using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektBD.Forms
{
    public partial class Komunikator : Form
    {
        string tymczasowaWiadomosc;
     
        public Komunikator()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProjektBD.Model.Student nowyKontakt = new ProjektBD.Model.Student();
            dodajKontakt(nowyKontakt);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int kontaktID = 0;
            usunKontakt(kontaktID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProjektBD.Model.Wiadomość wysylanaWiadomosc = new ProjektBD.Model.Wiadomość();

            wysylanaWiadomosc.nadawca = "user";
            wysylanaWiadomosc.treść = tymczasowaWiadomosc;
            wysylanaWiadomosc.dataWysłania = DateTime.Now;
            wysylanaWiadomosc.przeczytana = false;

            wysylanieWiadomosci(wysylanaWiadomosc);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tymczasowaWiadomosc = textBox1.Text;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Wyswietl wszystkie wiadomosci z danym rozmowca.
            ProjektBD.Model.Wiadomość pobranaWiadomosc;

            for (int i = 0; i < iloscWiadomosci; ++ i)
            {
                pobranaWiadomosc = pobierzWiadomosc(i);
                Console.WriteLine(pobranaWiadomosc.dataWysłania + " - " +
                                  pobranaWiadomosc.nadawca + " : " +
                                  pobranaWiadomosc.treść);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjektBD.Model.Użytkownik pobranyKontakt;

            for (int i = 0; i < iloscKontaktow; ++ i)
            {
                pobranyKontakt = pobierzKontakt(i);
                Console.WriteLine(i + ". " + pobranyKontakt.login);
            }
        }

        /**
          *  Obsluga wiadomosci.
          **/
        private void wysylanieWiadomosci(ProjektBD.Model.Wiadomość wiadomoscDoWyslania)
        {
            // ToDo
            // Zapisanie wiadomosci do bazy
            Console.WriteLine("Wysylanie wiadomosci od uzytkownika do adresata: " + wiadomoscDoWyslania.treść);
        }

        private ProjektBD.Model.Wiadomość pobierzWiadomosc(int wiadomoscID)
        {
            // ToDo
            // Pobranie wiadomosci z bazy
            ProjektBD.Model.Wiadomość pobranaWiadomosc = new ProjektBD.Model.Wiadomość();
            return pobranaWiadomosc;
        }


        /**
         *  Obsluga listy kontaktow.
         **/
        private ProjektBD.Model.Użytkownik pobierzKontakt(int kontaktID)
        {
            // ToDo
            // Pobranie kontaktu z bazy
            ProjektBD.Model.Użytkownik pobranyKontakt = new ProjektBD.Model.Student();
            return pobranyKontakt;
        }

        private void dodajKontakt(ProjektBD.Model.Użytkownik nowyKontakt)
        {
            // ToDo
            // Dodanie kontaktu do bazy
        }

        private void usunKontakt(int kontaktID)
        {
            // ToDo
            // Usuniecie kontaktu z bazy
        }
    }
}
