using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Model;

namespace ProjektBD.Forms.CommonForms
{
    public partial class SubjectDetails : Form
    {
        public SubjectDetails(PrzedmiotDetailsDTO subjectDetails)
        {
            InitializeComponent();

            label3.Text = subjectDetails.nazwa;
            label4.Text = subjectDetails.prowadzący;
            label7.Text = subjectDetails.liczbaStudentów.ToString();

            if (subjectDetails.maxLiczbaStudentów.HasValue)
                label7.Text += " / " + subjectDetails.maxLiczbaStudentów.Value;

            label9.Text = subjectDetails.opis;
        }

        // Zwykły AutoSize formularza nie ogarnie wszystkiego, co się tu dzieje
        // A jak już ogarnie, to brzydko
        private void labelSizeChanged(object sender, EventArgs e)
        {
            Label currentLabel = (sender as Label);

            int diffX = currentLabel.Size.Width + currentLabel.Location.X - this.Size.Width;
            int diffY = currentLabel.Size.Height + currentLabel.Location.Y - this.Size.Height;

            if (diffX > -25)
            {
                diffX += 25;                        // Poprawka na obramowanie formularza

                Size newSize = this.Size;
                newSize.Width += diffX;
                this.Size = newSize;

                Point newLocation = label1.Location;
                newLocation.X += diffX / 2;
                label1.Location = newLocation;
            }

            if (diffY > -50)
            {
                diffY += 50;                        // Poprawka na obramowanie i pasek tytułowy formularza

                Size newSize = this.Size;
                newSize.Height += diffY;
                this.Size = newSize;
            }
        }
    }
}
