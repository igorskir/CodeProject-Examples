//----------------------------------------------------------------------------
//
// THIS WORK IS PROVIDED "AS IS", "WHERE IS" AND "AS AVAILABLE", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED OR CONDITIONS OR GUARANTEES.
// YOU, THE USER, ASSUME ALL RISK IN ITS USE, INCLUDING COPYRIGHT INFRINGEMENT, PATENT INFRINGEMENT, SUITABILITY, ETC.
// AUTHOR EXPRESSLY DISCLAIMS ALL EXPRESS, IMPLIED OR STATUTORY WARRANTIES OR CONDITIONS, INCLUDING WITHOUT LIMITATION,
// WARRANTIES OR CONDITIONS OF MERCHANTABILITY, MERCHANTABLE QUALITY OR FITNESS FOR A PARTICULAR PURPOSE,
// OR ANY WARRANTY OF TITLE OR NON-INFRINGEMENT, OR THAT THE WORK (OR ANY PORTION THEREOF) IS CORRECT, USEFUL, BUG-FREE OR FREE OF VIRUSES.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Data;
using Microsoft.Windows.Controls;
using System.Windows.Threading;
using System.Data.SQLite;

namespace DataGridTemplateSample
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		// Delegates for void functions
		public delegate void VoidFunction<T>(T argument1);
		public delegate void VoidFunction<T, U>(T argument1, U argument2);

		private string SQLquery;
		private Thread currentThread;

		public Window1()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Sets the status label content in a thread safe way
		/// </summary>
		/// <param name="pos">Status label position</param>
		/// <param name="str">Status label content</param>
		private void SetStatus(int pos, string str)
		{
			if (statusLabel1.CheckAccess())
			{
				switch (pos)
				{
					case 1:
						statusLabel1.Text = str;
						break;
					case 2:
						statusLabel2.Text = str;
						break;
					case 3:
						statusLabel3.Text = str;
						break;
				}
			}
			else
			{
				VoidFunction<int, string> d = new VoidFunction<int, string>(SetStatus);
				//statusLabel1.Dispatcher.Invoke(d, DispatcherPriority.Normal, new object[] { pos, str });
				statusLabel1.Dispatcher.Invoke(DispatcherPriority.Normal, d, (object)pos, (object)str);
			}
		}

		/// <summary>
		/// Sets the DataView of a DataGrid in a thread safe way
		/// </summary>
		/// <param name="view">The DataView to set</param>
		private void SetGridView(DataView view)
		{
			if (dataGrid1.CheckAccess())
			{
				dataGrid1.ItemsSource = view;
			}
			else
			{
				VoidFunction<DataView> d = new VoidFunction<DataView>(SetGridView);
				dataGrid1.Dispatcher.Invoke(DispatcherPriority.Normal, d, (object)view);
			}
		}

		/// <summary>
		/// Enable/disable buttons in a thead safe way
		/// </summary>
		/// <param name="enabled">The value indication whether the buttons are enabled</param>
		private void SetBtnEnabled(bool enabled)
		{
			if (ExecuteSqlButton.CheckAccess())
			{
				ExecuteSqlButton.IsEnabled = enabled;
				SelectDbButton.IsEnabled = enabled;
			}
			else
			{
				VoidFunction<bool> d = new VoidFunction<bool>(SetBtnEnabled);
				ExecuteSqlButton.Dispatcher.Invoke(DispatcherPriority.Normal, d, (object)enabled);
			}
		}

		/// <summary>
		/// Execute SQL button handler
		/// </summary>
		private void ExecuteSQL_Click(object sender, RoutedEventArgs e)
		{
			// Disable all buttons
			SetBtnEnabled(false);

			// Check for query execution
			if (currentThread != null && currentThread.IsAlive)
			{
				return;
			}
			// Check if the query field is empty
			else if (textBox1.Text == string.Empty)
			{
				SetStatus(1, "Ready");
				SetStatus(2, "");
				SetStatus(3, "");
				SetBtnEnabled(true);
				return;
			}

			// Update the status labels
			SetStatus(1, "Executing sql query ...");
			SetStatus(2, "");
			SetStatus(3, "");

			// Store the SQL query
			this.SQLquery = textBox1.Text;

			// Execute thr query in a new thread
			currentThread = new Thread(new ThreadStart(ThreadExecuteSQL));
			currentThread.Start();
		}

		/// <summary>
		/// Execute the SQL query
		/// </summary>
		private void ThreadExecuteSQL()
		{
			// Default duration
			TimeSpan duration = new TimeSpan(0);

			SQLiteConnection dbconnectie = new SQLiteConnection(DataProvider.ConnectionString);
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(this.SQLquery, dbconnectie);

			DataTable table = new DataTable();

			try
			{
				// Open connection
				dbconnectie.Open();

				// Execute query and store execution time
				DateTime startTime = DateTime.Now;
				adapter.Fill(table);
				DateTime endTime = DateTime.Now;

				// Set the DataView and status
				duration = endTime - startTime;
				SetStatus(3, string.Format("{0} records", table.Rows.Count));
				SetGridView(table.DefaultView);
			}
			catch (SQLiteException ex)
			{
				// Show the exeception to the user
				MessageBox.Show(string.Format("Exception:\r\n{0}", ex.Message), "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
				SetStatus(3, "Exception");
			}
			finally
			{
				// Close connection
				dbconnectie.Close();
			}

			// Update status
			SetStatus(1, "Ready");
			SetStatus(2, String.Format("{0} seconds and {1} milliseconds", (int)(duration.TotalSeconds), duration.Milliseconds));

			// Enable the buttons
			SetBtnEnabled(true);
		}

		/// <summary>
		/// Select the database file to use
		/// </summary>
		private void SelectDatabase_Click(object sender, RoutedEventArgs e)
		{
			// Select database
			Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.Filter = "SQLite database (*.sqlite3;*.sqlite;*.db;*.bin)|*.sqlite3;*.sqlite;*.db;*.bin|All files (*.*)|*.*";
			dialog.Multiselect = false;

			// Only continue when user selects a file
			if (dialog.ShowDialog() != true || dialog.FileName == string.Empty)
			{
				return;
			}

			// Store the chosen filename
			DataProvider.Database = dialog.FileName;
			// Update window title
			this.Title = string.Format("DataGridTemplateSample - {0}", DataProvider.Database);
		}

		/// <summary>
		/// Window initalization code
		/// </summary>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Put the focus on the sql query field
			Keyboard.Focus(textBox1);
		}

		/// <summary>
		/// Prevent closing the window when executing a sql query
		/// </summary>
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Check for query execution
			if (currentThread != null && currentThread.IsAlive)
			{
				// Ask user what to do
				string msg = "Executing SQL query. Terminate query execution?";
				MessageBoxResult result =
				  MessageBox.Show(
					msg,
					"DataGridTemplateSample",
					MessageBoxButton.YesNo,
					MessageBoxImage.Exclamation);

				if (result == MessageBoxResult.Yes)
				{
					// Terminate thread when it is alive
					if (currentThread != null && currentThread.IsAlive)
					{
						currentThread.Abort();
					}
				}
				else
				{
					// If user doesn't want to close, cancel closure
					e.Cancel = true;
				}
			}
		}

		#region Hyperlink
		private void Hyperlink_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{
			/**
			 * Manually select cell
			 * Auto selection is done with OnContextMenuOpening() in DataGrid
			 * 'VisualTreeHelper.GetParent(sourceElement) as UIElement' failes for HyperLink so HandleSelectionForCellInput() is not called
			 * 
			 * StackTrace
			 * MakeCellSelection()
			 * HandleSelectionForCellInput()
			 * OnContextMenuOpening()
			 */
			DataGridCell cell = Helper.GetCell(e.OriginalSource as DependencyObject);
			Helper.SelectCell(dataGrid1, cell);
		}

		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink link = (Hyperlink)sender;
			string navigateUri = link.NavigateUri.ToString();
			try
			{
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(navigateUri));
			}
			catch (System.ComponentModel.Win32Exception ex)
			{
				MessageBox.Show(ex.Message, "Win32Exception", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			e.Handled = true;
		}

		#region Hyperlink hover
		private string originalStatus = string.Empty;
		private void Hyperlink_MouseEnter(object sender, MouseEventArgs e)
		{
			originalStatus = statusLabel1.Text;
			Hyperlink link = sender as Hyperlink;

			statusLabel1.Text = link.NavigateUri.ToString();
		}

		private void Hyperlink_MouseLeave(object sender, MouseEventArgs e)
		{
			statusLabel1.Text = originalStatus;
		}
		#endregion
		#endregion

		#region Datagrid edit
		private DataGridRow _currentEditingRow;
		private bool IsContextMenuOpen;

		public bool IsEditingRow
		{
			get { return (bool)GetValue(EditingRowProperty); }
			set { SetValue(EditingRowProperty, value); }
		}

		public static readonly DependencyProperty EditingRowProperty =
			DependencyProperty.Register(
			  "IsEditingRow",
			  typeof(bool),
			  typeof(Window1),
			  new FrameworkPropertyMetadata(false, null, null));

		private void dataGrid1_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
		{
			if (e.EditAction == DataGridEditAction.Cancel)
			{
				// Cancelling the entire row will reset the state
				IsEditingRow = false;
			}
			else if (e.EditAction == DataGridEditAction.Commit)
			{
				e.Cancel = IsEditingRow;
			}
		}

		private void dataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			DataRowView drv = dataGrid1.CurrentItem as DataRowView; // DataRow
			if (drv == null) return;

			if (drv.IsEdit && e.Key == System.Windows.Input.Key.Enter)
			{
				// only 'Enter' key on an editing row will allow a commit to occur
				IsEditingRow = false;
			}
		}

		private void dataGrid1_BeginningEdit(object sender, Microsoft.Windows.Controls.DataGridBeginningEditEventArgs e)
		{
			// When pressing escape on a selected auto generated textblock, the row goes in edit mode
			// DataGridCell->OnTextInput [SendInputToColumn] DataGridColumn->OnInput
			// In case of text block => DataGridTextColumn->OnInput

			// when the row is not yet in edit mode, prevent edit mode when pressing esc
			if (!IsEditingRow && e.Column.GetType() == typeof(DataGridTextColumn) && e.EditingEventArgs.RoutedEvent == UIElement.TextInputEvent)
			{
				if (((InputEventArgs)e.EditingEventArgs).Device.GetType().BaseType == typeof(KeyboardDevice))
				{
					KeyboardDevice device = (KeyboardDevice)((InputEventArgs)e.EditingEventArgs).Device;
					if (device.IsKeyDown(Key.Escape))
					{
						e.Cancel = true;
						return;
					}
				}
			}

			if (!IsEditingRow)
			{
				// set edit mode state
				IsEditingRow = true;
				_currentEditingRow = e.Row;
			}
			else if (e.Row != _currentEditingRow)
			{
				// cancel all new edits for different rows
				e.Cancel = true;
			}
		}

		/**
		 * Context Menu fix:
		 * TextBox context menu is inefective because datagrid editing is ended (focus lost to context menu)
		 * DataGrid editing ending is prevented by handling CellEditEnding
		 * To detect context menu opening handle ContextMenuOpening
		 * By default ContextMenuOpening would not be catched, ContextMenuOpening is handled by TextEditorContextMenu.OnContextMenuOpening
		 * To catch ContextMenuOpening set a custom contextmenu
		 * Global style for textbox is not applied to the textbox inside DataGridTextColumn
		 * DataGridTextColumn.EditingElementStyle (with custom contextmenu) is set in the AutoGeneratingColumn event
		 */

		private void dataGrid1_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
			IsContextMenuOpen = true;
			return;
		}

		private void dataGrid1_ContextMenuClosing(object sender, ContextMenuEventArgs e)
		{
			IsContextMenuOpen = false;
			return;
		}

		private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			// prevent ending edit modus, when focus is lost to context menu
			if (e.EditAction == DataGridEditAction.Commit)
			{
				e.Cancel = IsContextMenuOpen;
			}

			return;
		}
		#endregion

		#region ClipBoard

		// ApplicationCommands.Copy catched by CommandBinding in ContextMenu.CommandBindings (default catched by DataGrid.OnExecutedCopy)

		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (dataGrid1.ClipboardCopyMode == DataGridClipboardCopyMode.None)
			{
				e.CanExecute = true;
				e.Handled = true;
			}
			return;
		}

		private void Column_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MenuItem item = e.Source as MenuItem;
			string content = "";

			// get the column index
			int index = Helper.GetColumnIndex(dataGrid1, item.DataContext);
			// check if the index is valid
			if (index < 0)
			{
				return;
			}

			// get the data column index
			index = Helper.GetDataColumnIndex(dataGrid1, dataGrid1.Columns[index].SortMemberPath);
			// check index
			if (index < 0)
			{
				return;
			}

			for (int i = 0; i < (dataGrid1.Items.Count - 1); i++)
			{
				DataRowView dataRowView = (DataRowView)dataGrid1.Items[i];
				string data = Convert.ToString(dataRowView.Row.ItemArray[index]);

				// add new line
				if (i >= 1)
				{
					content += Environment.NewLine;
				}

				content += data;
			}

			Clipboard.SetData(DataFormats.Text, content);
			return;
		}

		private void Cell_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			int index;
			string content = "";
			for (int i = 0; i < dataGrid1.SelectedCells.Count; i++)
			{
				DataGridCellInfo info = dataGrid1.SelectedCells[i];
				DataRowView dataRowView = (DataRowView)info.Item;

				// add new line
				if (i >= 1)
				{
					content += Environment.NewLine;
				}

				// Get the column index
				index = Helper.GetDataColumnIndex(dataGrid1, info.Column.SortMemberPath);
				// make sure the index is valid
				if (index != -1)
				{
					content += Convert.ToString(dataRowView.Row.ItemArray[index]);
				}
			}

			Clipboard.SetData(DataFormats.Text, content);
			return;
		}

		private void Row_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;

			int index;
			DataRowView current = null;
			string content = "";

			for (int i = 0; i < dataGrid1.SelectedCells.Count; i++)
			{
				DataGridCellInfo info = dataGrid1.SelectedCells[i];
				DataRowView dataRowView = (DataRowView)info.Item;
				if (current != null && current != dataRowView)
				{
					content += Environment.NewLine;
				}
				current = dataRowView;

				// add new line
				if (i >= 1)
				{
					content += Environment.NewLine;
				}

				// Get the column index
				index = Helper.GetDataColumnIndex(dataGrid1, info.Column.SortMemberPath);
				// make sure the index is valid
				if (index != -1)
				{
					content += Convert.ToString(dataRowView.Row.ItemArray[index]);
				}
			}

			Clipboard.SetData(DataFormats.Text, content);
			return;
		}
		#endregion
	}
}
