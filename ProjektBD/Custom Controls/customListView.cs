using System;
using System.Collections.Generic;
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
        public customListView()
        {
            this.Font = new Font("Microsoft Sans Serif", 9);
            this.FullRowSelect = true;
            this.GridLines = true;
            this.View = View.Details;
            //this.MultiSelect = false;
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
                    this.Items.Add(propertiesList[0].GetValue(d).ToString());             // 0 - pierwsza właściwość (kolumna), d - który element listy (wiersz)

                //---------DODAWANIE ITEMÓW (kolumny)---------
                int i = 0;
                foreach (ListViewItem item in this.Items)
                {
                    for (int j = 1; j < propertiesList.Count; j++)
                    {
                        object subItem = propertiesList[j].GetValue(data[i]);               // j-ta właściwość z i-tego elementu listy

                        if (subItem != null)
                            item.SubItems.Add(subItem.ToString());
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
        }
    }
}