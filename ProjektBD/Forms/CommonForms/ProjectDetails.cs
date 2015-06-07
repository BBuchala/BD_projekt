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
    public partial class ProjectDetails : Form
    {
        public ProjectDetails(ProjektDetailsDTO projectDetails)
        {
            InitializeComponent();

            label3.Text = projectDetails.nazwa;
            label5.Text = projectDetails.nazwaPrzedmiotu;
            label7.Text = projectDetails.liczbaStudentów.ToString() + " / " + projectDetails.maxLiczbaStudentów.ToString();
            label9.Text = projectDetails.opis;
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
