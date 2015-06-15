using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.DAL;
using ProjektBD.Databases;
using ProjektBD.Forms;
using ProjektBD.Forms.CommonForms;
using ProjektBD.Model;
using ProjektBD.Utilities;

namespace ProjektBD.Custom_Controls
{
    class customListView : ListView
    {
        #region Pola i konstruktor
        //----------------------------------------------------------------

        public Color previouslySelectedItemColor = new Color();

        /// <summary>
        /// Typ danych przechowywanych przez kontrolkę
        /// </summary>
        private listViewType dataType;

        /// <summary>
        /// ID oceny, dla której pobierane są szczegóły
        /// </summary>
        public long gradeID;

        /// <summary>
        /// ID rozmowy, dla której pobierane są szczegóły
        /// </summary>
        public int conversationID;

        private enum listViewType
        {
            StudentsWithIndexNumber,
            StudentsWithLogin,
            TeachersWithLogin,
            Subjects,
            Projects,
            Grades,
            Contacts
        };

        public customListView()
        {
            this.Font = new Font("Microsoft Sans Serif", 9);
            this.FullRowSelect = true;
            this.GridLines = true;
            this.View = View.Details;
            this.MultiSelect = false;

            this.Enter += loadItemState;
            this.Leave += saveItemState;
        }

        //----------------------------------------------------------------
        #endregion

        #region Wypełnienie danymi, tworzenie rozwijalnego menu, ustawienie typu
        //----------------------------------------------------------------

        /// <summary>
        /// Wypełnia listView danymi z listy podanej przez parametr
        /// </summary>
        /// <typeparam name="T">Typ klasy encji, której elementy znajdują się w liście</typeparam>
        public void fill<T>(List<T> data) where T : class
        {
            this.Clear();                                     // Usuwa wszystkie wiersze i kolumny z kontrolki, jeśli istnieją

            PropertyInfo[] właściwości = typeof(T).GetPropertiesForListView();

            //---------DODAWANIE KOLUMN---------
            foreach (PropertyInfo wł in właściwości)
                this.Columns.Add(wł.Name);

            if (data.Count > 0)
            {
                List<PropertyInfo> propertiesList = właściwości.ToList();               // Lista właściwości
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

            setListViewType<T>();
            createMenuStrip();
        }

        /// <summary>
        /// Tworzy rozwijane menu w zależności od rodzaju danych przechowywanych przez kontrolkę
        /// </summary>
        private void createMenuStrip()
        {
            ContextMenuStrip cms = new ContextMenuStrip();

            if (this.dataType == listViewType.Subjects)
            {
                cms.Items.Add("Pokaż szczegóły");
                cms.Items.Add("Pokaż profil prowadzącego");
                cms.Items.Add("Wyślij wiadomość do prowadzącego");
            }

            else if (this.dataType == listViewType.Contacts)
                cms.Items.Add("Pokaż szczegóły rozmowy");

            else
            {
                foreach (ColumnHeader column in this.Columns)
                {
                    if (column.Text.Equals("login"))
                    {
                        cms.Items.Add("Pokaż profil");

                        // Blokuje możliwość wysłania wiadomości, gdy komunikator jest już otwarty
                        var openedGGForms = Application.OpenForms.OfType<Komunikator>().ToList();

                        if (openedGGForms.Count == 0)
                            cms.Items.Add("Wyślij wiadomość");

                        break;
                    }
                }
            }
            
            if (cms.Items.Count == 0)
                cms.Items.Add("Pokaż szczegóły");

            // Przy otwieraniu menusa na PPM sprawdza, czy jest zaznaczony jakikolwiek item. Jeśli nie, anuluje event
            cms.Opening += (s, e) =>
                {
                    if ( this.SelectedItems.Count == 0 )
                        e.Cancel = true;
                };

            cms.ItemClicked += ContextMenuStrip_ItemClicked;

            this.ContextMenuStrip = cms;
        }

        /// <summary>
        /// Ustawia typ danych wyświetlanych przez kontrolkę
        /// </summary>
        /// <typeparam name="T">Typ danych, jakimi wypełniona została kontrolka</typeparam>
        private void setListViewType<T>() where T: class
        {
            string typeName = typeof(T).Name;

            switch (typeName)
            {
                case "ProwadzącyDTO":
                    dataType = listViewType.TeachersWithLogin;
                    return;

                case "StudentDTO":
                    dataType = listViewType.StudentsWithIndexNumber;
                    return;

                case "PrzedmiotDTO":
                case "PrzedmiotProwadzącegoDTO":
                    dataType = listViewType.Subjects;
                    return;

                case "ProjektDTO":
                case "ForeignProjektDTO":
                    dataType = listViewType.Projects;
                    return;

                case "OcenaDTO": 
                case "OcenaZProjektuDTO":
                    dataType = listViewType.Grades;
                    return;

                case "KontaktyDTO":
                    dataType = listViewType.Contacts;
                    return;
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Zaznaczanie itemów
        //----------------------------------------------------------------

        void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Form newForm = new Form();
            dynamic dataForForm;                             // Typ określi sobie podczas wykonywania programu

            customListViewDatabase db = new customListViewDatabase();
            string selectedItem = this.SelectedItems[0].Text;
            string selectedMenuStripItem = e.ClickedItem.Text;

            // Zabezpieczenie przed bullshitem powstającym przy otwieraniu profilu prowadzących
            if ( selectedMenuStripItem.Equals("Pokaż profil") )
                correctDataType();

            switch (selectedMenuStripItem)
            {
                case "Pokaż profil":
                    switch (dataType)
                    {
                        case listViewType.StudentsWithIndexNumber:
                            int nrIndeksu = Int32.Parse(selectedItem);

                            dataForForm = db.getStudentProfileData(nrIndeksu);
                            newForm = new StudentProfileForm(dataForForm);
                            break;

                        case listViewType.StudentsWithLogin:
                            string studentLogin = selectedItem;

                            dataForForm = db.getStudentProfileData(studentLogin);
                            newForm = new StudentProfileForm(dataForForm);
                            break;

                        case listViewType.TeachersWithLogin:
                            string teacherLogin = selectedItem;

                            dataForForm = db.getTeacherProfileData(teacherLogin);
                            newForm = new TeacherProfileForm(dataForForm);
                            break;
                    }
                    break;

                case "Pokaż profil prowadzącego":
                    string subject = selectedItem;

                    dataForForm = db.getTeacherProfileFromSubject(subject);
                    newForm = new TeacherProfileForm(dataForForm);
                    break;

                case "Pokaż szczegóły":
                    switch (dataType)
                    {
                        case listViewType.Subjects:
                            string subjectName = selectedItem;

                            dataForForm = db.getSubjectDetails(subjectName);
                            newForm = new SubjectDetails(dataForForm);
                            break;
                            
                        case listViewType.Projects:
                            string projectName = selectedItem;

                            dataForForm = db.getProjectDetails(projectName);
                            newForm = new ProjectDetails(dataForForm);
                            break;

                        case listViewType.Grades:
                            dataForForm = db.getGradeDetails(gradeID);
                            newForm = new GradeDetails(dataForForm);
                            break;
                    }
                    break;

                case "Pokaż szczegóły rozmowy":
                    dataForForm = db.getConversationDetails(conversationID);
                    newForm = new ConversationDetails(dataForForm);
                    break;
            }

            newForm.ShowDialog();
            newForm.Dispose();

            db.disposeContext();
        }

        /// <summary>
        /// Ustawia prawidłowy typ danych w kontrolce w przypadku próby otwarcia profilu użytkownika
        /// </summary>
        private void correctDataType()
        {
            foreach (ColumnHeader col in this.Columns)
            {
                if (col.Text.Equals("stanowisko"))
                {
                    foreach (ListViewItem.ListViewSubItem item in this.SelectedItems[0].SubItems)
                    {
                        if (item.Text.Equals("Student"))
                        {
                            dataType = listViewType.StudentsWithLogin;
                            break;
                        }
                        else if (item.Text.Equals("Prowadzący"))
                        {
                            dataType = listViewType.TeachersWithLogin;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        //----------------------------------------------------------------
        #endregion

        #region Zmiana kolorów
        //----------------------------------------------------------------

        /// <summary>
        /// Po wyjściu z kontrolki zapamiętuje zaznaczony item, tymczasowo zmieniając jego kolor.
        /// </summary>
        private void saveItemState(object sender, EventArgs e)
        {
            if (this.SelectedItems.Count > 0)
            {
                this.previouslySelectedItemColor = this.SelectedItems[0].BackColor;

                foreach (ListViewItem item in this.SelectedItems)
                    item.BackColor = Color.AntiqueWhite;
            }
        }

        /// <summary>
        /// Przywraca poprzedni kolor zaznaczonego itemu, zmieniony podczas przechodzenia do innej listy.
        /// </summary>
        private void loadItemState(object sender, EventArgs e)
        {
            if (this.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in this.SelectedItems)
                    item.BackColor = this.previouslySelectedItemColor;
            }
        }

        //----------------------------------------------------------------
        #endregion
    }
}