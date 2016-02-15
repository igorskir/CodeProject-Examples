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
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Data;

namespace DataGridTemplateSample
{
	// http://blogs.msdn.com/vinsibal/archive/2008/08/11/net-3-5-sp1-and-wpf-datagrid-ctp-is-out-now.aspx
	class Helper
	{
		public static DataGridCell GetCell(DataGrid dataGrid, int row, int column)
		{
			DataGridRow rowContainer = GetRow(dataGrid, row);
			if (rowContainer != null)
			{
				DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

				// try to get the cell but it may possibly be virtualized
				DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
				if (cell == null)
				{
					// now try to bring into view and retreive the cell
					dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);

					cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
				}

				return cell;
			}

			return null;
		}

		public static DataGridCell GetCell(DataGrid dataGrid, int row, object column)
		{
			int index = -1;

			for (int i = 0; i < dataGrid.Columns.Count; i++)
			{
				if (dataGrid.Columns[i].Header == column)
				{
					index = i;
					break;
				}
			}

			if (index >= 0) {
				return GetCell(dataGrid, row, index);
			}

			return null;
		}

		/// <summary>
		/// Get the DataGridCell container for the specfied DependencyObject
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static DataGridCell GetCell(DependencyObject obj)
		{
			for (; obj != null; obj = VisualTreeHelper.GetParent(obj) as DependencyObject)
			{
				if (obj is DataGridCell)
				{
					return obj as DataGridCell;
				}
			}

			return null;
		}

		public static void SelectCell(DataGrid dataGrid, DataGridCell cell)
		{
			bool extendSelection = dataGrid.SelectionMode == DataGridSelectionMode.Extended;
			bool minimalModify = ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control);

			if (!extendSelection)
			{
				return;
			}

			if (minimalModify)
			{
				//DataGridCellInfo info = new DataGridCellInfo(cell);
				//dataGrid1.SelectedCells.Add(info);
				cell.IsSelected = true;
			}
			else
			{
				dataGrid.UnselectAllCells();
				//DataGridCellInfo info = new DataGridCellInfo(cell);
				//dataGrid1.SelectedCells.Add(info);
				cell.IsSelected = true;
			}

			cell.Focus();
			return;
		}

		/// <summary>
		/// Get the (DataGrid) column index for the specified column name
		/// </summary>
		/// <param name="dataGrid"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public static int GetColumnIndex(DataGrid dataGrid, object column)
		{
			int index = -1;

			for (int i = 0; i < dataGrid.Columns.Count; i++)
			{
				if (dataGrid.Columns[i].Header == column)
				{
					index = i;
					break;
				}
			}

			return index;
		}

		/// <summary>
		/// Get the raw column index for the specified column name
		/// </summary>
		/// <param name="dataGrid"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public static int GetDataColumnIndex(DataGrid dataGrid, string column)
		{
			int index = -1;

			DataView view = dataGrid.ItemsSource as DataView;
			// sanity check
			if (view == null) { return index; }

			for (int i = 0; i < view.Table.Columns.Count; i++)
			{
				if (view.Table.Columns[i].ColumnName == column)
				{
					index = i;
					break;
				}
			}

			return index;
		}

		public static DataGridRow GetRow(DataGrid dataGrid, int index)
		{
			DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
			if (row == null)
			{
				// may be virtualized, bring into view and try again
				dataGrid.ScrollIntoView(dataGrid.Items[index]);
				dataGrid.UpdateLayout();

				row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
			}

			return row;
		}

		public static T GetVisualChild<T>(Visual parent) where T : Visual
		{
			T child = default(T);

			int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < numVisuals; i++)
			{
				Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
				child = v as T;
				if (child == null)
				{
					child = GetVisualChild<T>(v);
				}
				if (child != null)
				{
					break;
				}
			}

			return child;
		}

		public static T GetVisualChild<T>(Visual parent, int index) where T : Visual
		{
			T child = default(T);

			int encounter = 0;
			Queue<Visual> queue = new Queue<Visual>();
			queue.Enqueue(parent);
			while (queue.Count > 0)
			{
				Visual v = queue.Dequeue();
				child = v as T;
				if (child != null)
				{
					if (encounter == index)
						break;
					encounter++;
				}
				else
				{
					int numVisuals = VisualTreeHelper.GetChildrenCount(v);
					for (int i = 0; i < numVisuals; i++)
					{
						queue.Enqueue((Visual)VisualTreeHelper.GetChild(v, i));
					}
				}
			}

			return child;
		}
	}
}
