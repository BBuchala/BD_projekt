using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektBD.Utilities
{
    /// <summary>
    /// Klasa ułatwiająca wypisywanie MessageBox'ów
    /// </summary>
    static class MsgBoxUtils
    {
        /// <summary>
        /// Funkcja do wyświetlania MsgBoxa z warningiem.
        /// </summary>
        public static void displayWarningMsgBox(string title, string text)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Funkcja do wyświetlania MsgBoxa z informacją.
        /// </summary>
        public static void displayInformationMsgBox(string title, string text)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Funkcja do wyświetlania MsgBoxa z errorem.
        /// </summary>
        public static void displayErrorMsgBox(string title, string text)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Funkcja do wyświetlania MsgBoxa z zapytaniem.
        /// </summary>
        public static DialogResult displayQuestionMsgBox(string title, string text, Form owner)
        {
            return MessageBox.Show(owner, text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Funkcja do wyświetlania MsgBoxa z warningiem o braku połączenia z bazą.
        /// </summary>
        public static void displayConnectionErrorMsgBox()
        {
            MessageBox.Show("Nie można nawiązać połączenia z bazą danych. Spróbuj ponownie później", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
