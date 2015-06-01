using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Model;

namespace ProjektBD.Custom_Controls
{
    class customListView : ListView
    {
        public Color previouslySelectedItemColor = new Color();

        public customListView()
        {
            this.Font = new Font("Microsoft Sans Serif", 9);
            this.FullRowSelect = true;
            this.GridLines = true;
            this.View = View.Details;
            this.MultiSelect = false;
        }

        /// <summary>
        /// Wypełnia listView danymi z listy podanej przez parametr
        /// </summary>
        /// <typeparam name="T">Typ klasy encji, której elementy znajdują się w liście</typeparam>
        public void fill<T>(List<T> data) where T : class
        {
            this.Clear();                                     // Usuwa wszystkie wiersze i kolumny z kontrolki, jeśli istnieją

            PropertyInfo[] właściwości = typeof(T).GetProperties();

            //---------DODAWANIE KOLUMN---------
            foreach (PropertyInfo wł in właściwości)
                this.Columns.Add(wł.Name);

            if (data.Count > 0)
            {
                List<PropertyInfo> propertiesList = data[0].GetType().GetProperties().ToList();      // Lista właściwości
                //propertiesList[0].GetValue(data[0]);            // Która właściwość zostanie pobrana / z której linijki

                //---------DODAWANIE ITEMÓW (wiersze)---------
                foreach (var d in data)
                {
                    object itemValue = propertiesList[0].GetValue(d);

                    if (itemValue != null)
                        this.Items.Add( itemValue.ToString() );           // 0 - pierwsza właściwość (kolumna), d - który element listy (wiersz)
                    else
                        this.Items.Add("");
                }

                //---------DODAWANIE ITEMÓW (kolumny)---------
                int i = 0;
                foreach (ListViewItem item in this.Items)
                {
                    for (int j = 1; j < propertiesList.Count; j++)
                    {
                        object subItemValue = propertiesList[j].GetValue(data[i]);               // j-ta właściwość z i-tego elementu listy

                        if (subItemValue != null)
                            item.SubItems.Add( subItemValue.ToString() );
                        else
                            item.SubItems.Add("");
                    }

                    if (i % 2 == 1)
                        item.BackColor = Color.FromArgb(255, 235, 235, 235);

                    i++;
                }
            }

            //---------WYŚRODKOWANIE I AUTOSIZE KOLUMN---------
            foreach (ColumnHeader column in this.Columns)
            {
                column.TextAlign = HorizontalAlignment.Center;
                column.Width = -2;                              // Automatyczne wyrównanie do najdłuższego itemu w kolumnie
            }

            createMenuStrip();
        }

        /// <summary>
        /// Tworzy rozwijane menu w zależności od rodzaju danych przechowywanych przez kontrolkę
        /// </summary>
        private void createMenuStrip()
        {
            ContextMenuStrip cms = new ContextMenuStrip();

            foreach (ColumnHeader column in this.Columns)
            {
                if (column.Text.Equals("login"))
                {
                    cms.Items.Add("Pokaż profil");
                    cms.Items.Add("Wyślij wiadomość");
                    break;
                }
                else if (column.Text.Equals("prowadzący"))
                {
                    cms.Items.Add("Pokaż szczegóły");
                    cms.Items.Add("Wyślij wiadomość do prowadzącego");
                    break;
                }
            }

            if (cms.Items.Count < 2)
                cms.Items.Add("Pokaż szczegóły");

            // Przy otwieraniu menusa na PPM sprawdza, czy jest zaznaczony jakikolwiek item. Jeśli nie, anuluje event
            cms.Opening += (s, e) =>
                {
                    if ( this.SelectedItems.Count == 0 )
                        e.Cancel = true;
                };

            this.ContextMenuStrip = cms;
        }

        /// <summary>
        /// Po wyjściu z kontrolki zapamiętuje zaznaczony item, tymczasowo zmieniając jego kolor.
        /// </summary>
        public void saveItemState()
        {
            if (this.SelectedItems.Count > 0)
            {
                this.previouslySelectedItemColor = this.SelectedItems[0].BackColor;
                this.SelectedItems[0].BackColor = Color.AntiqueWhite;
            }
        }

        /// <summary>
        /// Przywraca poprzedni kolor zaznaczonego itemu, zmieniony podczas przechodzenia do innej listy.
        /// </summary>
        public void loadItemState()
        {
            if (this.SelectedItems.Count > 0)
                this.SelectedItems[0].BackColor = this.previouslySelectedItemColor;
        }
    }
}