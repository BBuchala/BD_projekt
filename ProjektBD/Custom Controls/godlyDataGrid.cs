using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjektBD.Controllers;
using ProjektBD.Utilities;

namespace ProjektBD.Custom_Controls
{
    /// <summary>
    /// DataGridView służący do przeglądania i modyfikowania dowolnej wartości w bazie oraz dodawania i usuwania rekordów.
    /// <para> Przed skorzystaniem wymagane jest uruchomienie metody provideParams. </para>
    /// </summary>
    class godlyDataGrid : DataGridView
    {
        /// <summary>
        /// Ilość wierszy wyświetlanej tabeli
        /// </summary>
        private int rowsCount;

        /// <summary>
        /// Pozycja ostatnio odwiedzonej prawidłowo wypełnionej komórki
        /// </summary>
        private Point lastValidDatagridCell;

        /// <summary>
        /// Określa, czy użytkownik wpisuje nowy rekord do bazy
        /// </summary>
        private bool isInsertingNewRow = false;

        /// <summary>
        /// Ostatnia poprawna wartość w komórce
        /// </summary>
        private object recentValue;

        /// <summary>
        /// Kontroler formularza administratora
        /// </summary>
        private AdminController formController;

        public godlyDataGrid()
        {
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Dodaje metody wykonywane przy pojawieniu się danego zdarzenia.
            // Jeśli obsłużymy owe zdarzenia również w formularzu, te metody wykonają się przed nimi.
            this.CellBeginEdit += godlyDataGrid_CellBeginEdit;
            this.CellEndEdit += godlyDataGrid_CellEndEdit;
            this.CurrentCellChanged += godlyDataGrid_CurrentCellChanged;
            this.DataError += godlyDataGrid_DataError;
            this.UserAddedRow += godlyDataGrid_UserAddedRow;
            this.UserDeletedRow += godlyDataGrid_UserDeletedRow;
        }

        /// <summary>
        /// Dostarcza parametrów startowych kontrolce
        /// </summary>
        /// <param name="controller"> Kontroler formularza, w którym umieszczona zostanie kontrolka. </param>
        /// <param name="rowsCount"> Ilość rekordów tabeli, na której będą wykonywane operacje. </param>
        public void provideParams(AdminController controller, int rowsCount)
        {
            this.formController = controller;
            this.rowsCount = rowsCount;
        }

        private void godlyDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            recentValue = this[e.ColumnIndex, e.RowIndex].Value;
        }

        /// <summary>
        /// Gdy edytujemy komórkę w wierszu, który nie jest dodawany do bazy
        /// </summary>
        private void godlyDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // jeśli nie jest edytowany wiersz, którego nie ma jeszcze w bazie
                if ( this.CurrentCell.RowIndex < rowsCount - 1 || isInsertingNewRow == false )
                    formController.saveContext();
            }
            catch (DbUpdateException)
            {
                MsgBoxUtils.displayErrorMsgBox("Błąd", "Wprowadzono błędną wartość. Upewnij się, czy dane w kolumnie nie muszą być unikalne");

                this[e.ColumnIndex, e.RowIndex].Value = recentValue;

                this.RefreshEdit();                    // Odświeża zawartość komórki i cofa zmiany dokonane w kontekście
            }
        }

        /// <summary>
        /// Gdy przejdziemy do innej komórki
        /// </summary>
        private void godlyDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                //------------------------------------
                // Sprawdza, czy został naciśnięty ENTER na ostatnim, dodawanym właśnie do kontekstu wierszu
                if ( this.CurrentCell != null && this.CurrentCell.RowIndex == rowsCount && formController.doesContextHaveChanges() )
                {
                    formController.saveContext();

                    isInsertingNewRow = false;
                    lastValidDatagridCell = this.CurrentCellAddress;

                    this.Refresh();
                }

                //------------------------------------
                // Sprawdza, czy user nie wyszedł z wiersza przed zcommitowaniem danych
                else if ( isInsertingNewRow && this.CurrentCellAddress.Y != lastValidDatagridCell.Y )
                {
                    MsgBoxUtils.displayErrorMsgBox("Błąd", "Nie zatwierdzono nowo wprowadzonych danych.\n" +
                                                            "Aby zatwierdzić, wypełnij wszystkie wymagane pola i naciśnij klawisz ENTER.");

                    moveToTheLastValidCell();
                }

                //------------------------------------
                // Jeśli wszystko w porządku - aktualizuje pozycję ostatnio odwiedzonej prawidłowej komórki
                else
                    lastValidDatagridCell = this.CurrentCellAddress;
            }

            catch (DbEntityValidationException)
            {
                MsgBoxUtils.displayErrorMsgBox("Błąd", "Nie wszystkie wymagane wartości zostały podane.");

                moveToTheLastValidCell();
            }
            catch (DbUpdateException)
            {
                MsgBoxUtils.displayErrorMsgBox("Błąd", "Wprowadzono niepoprawną wartość klucza obcego.");

                moveToTheLastValidCell();
            }
            catch (InvalidOperationException)
            {
                MsgBoxUtils.displayErrorMsgBox("Błąd", "Wystąpił błąd. Upewnij się, że klucz obcy został wprowadzony prawidłowo.");

                moveToTheLastValidCell();
            }
        }

        private void godlyDataGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            rowsCount++;

            isInsertingNewRow = true;
        }

        private void godlyDataGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            rowsCount--;

            formController.saveContext();
        }

        private void godlyDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MsgBoxUtils.displayErrorMsgBox("Błąd", "Nieprawidłowy format wprowadzonych danych.");
        }

        /// <summary>
        /// Zapożyczone ze stack'a. Dodaje do kolejki zdarzeń nową wiadomość, która wykona się kiedyś po tym zdarzeniu.
        /// <para> Wykorzystane, bo w procedurze obsługi tego zdarzenia nie można zmienić aktualnej komórki, </para>
        /// <para> a nie ma za bardzo do czego podpiąć EventHandler'a. </para>
        /// </summary>
        private void moveToTheLastValidCell()
        {
            int x = lastValidDatagridCell.X;
            int y = lastValidDatagridCell.Y;

            BeginInvoke( (Action)delegate
            {
                this.CurrentCell = this.Rows[y].Cells[x];
                this.BeginEdit(true);
            });
        }
    }
}