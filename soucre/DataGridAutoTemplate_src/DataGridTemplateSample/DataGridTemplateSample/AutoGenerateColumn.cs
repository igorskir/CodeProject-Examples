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
using Microsoft.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;

namespace DataGridTemplateSample
{
	public class AutoGenerateColumnCollection : ObservableCollection<AutoGenerateColumn>
	{
		internal AutoGenerateColumnCollection(DependencyObject owner)
		{
			_ownerRef = new WeakReference(owner);
		}

		private WeakReference _ownerRef;
		public DependencyObject Owner
		{
			get { return _ownerRef.Target as DependencyObject; }
		}
	}

	public class AutoGenerateColumn
	{
		public string Column
		{
			get; set;
		}

		public DataTemplate CellTemplate
		{
			get; set;
		}

		public DataTemplate CellEditingTemplate
		{
			get; set;
		}

		public System.Windows.Data.BindingBase Binding
		{
			get; set;
		}
	}

	public class GenerateTemplateColumn
	{
		public static void _grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			DataGrid grid = sender as DataGrid;
			if (grid == null) { return; }

			AutoGenerateColumnCollection coll = GetColumns(grid);
			
			foreach (AutoGenerateColumn col in coll)
			{
				if (e.PropertyName == col.Column)
				{
					CustomDataGridTemplateColumn templateColumn = new CustomDataGridTemplateColumn();
					templateColumn.Header = e.Column.Header;
					if (col.CellTemplate != null)
					{
						templateColumn.CellTemplate = col.CellTemplate;
					}
					if (col.CellEditingTemplate != null)
					{
						templateColumn.CellEditingTemplate = col.CellEditingTemplate;
					}
					if (col.Binding != null)
					{
						templateColumn.Binding = col.Binding;
					}
					templateColumn.SortMemberPath = e.Column.SortMemberPath;

					e.Column = templateColumn;
					return;
				}
			}

			if (e.Column.GetType() == typeof(DataGridTextColumn))
			{
				// Set the edit style to a style containing a custom context menu in order to handle ContextMenuOpening
				((DataGridTextColumn)e.Column).EditingElementStyle = (Style)Application.Current.Resources["TextColumnEditStyle"];
				return;
			}

			return;
		}

		private static readonly DependencyProperty ColumnsProperty =
		   DependencyProperty.RegisterAttached("ColumnsInternal", typeof(AutoGenerateColumnCollection), typeof(GenerateTemplateColumn),
			   new FrameworkPropertyMetadata((GenerateTemplateColumn)null,
				   new PropertyChangedCallback(OnColumnsChanged)));

		private static AutoGenerateColumnCollection GetColumnsInternal(DependencyObject d)
		{
			var collection = (AutoGenerateColumnCollection)d.GetValue(ColumnsProperty);
			if (collection == null)
			{
				collection = new AutoGenerateColumnCollection(d);
				d.SetValue(ColumnsProperty, collection);
			}
			return collection;
		}

		/// <summary>
		/// Gets the Columns property.  This dependency property 
		/// indicates whether to use templates for the specified column.
		/// </summary>
		public static AutoGenerateColumnCollection GetColumns(DependencyObject d)
		{
			return GetColumnsInternal(d);
		}

		///// <summary>
		///// Sets the Columns property.  This dependency property 
		///// indicates whether to use templates for the specified column.
		///// </summary>
		//public static void SetColumns(DependencyObject d, AutoGenerateColumnCollection value)
		//{
		//    d.SetValue(ColumnsProperty, value);
		//}

		private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				AutoGenerateColumnCollection coll = e.NewValue as AutoGenerateColumnCollection;
				//coll.CollectionChanged += AutoGenerateColumn_CollectionChanged;

				DataGrid grid = coll.Owner as DataGrid;
				if (grid != null)
				{
					grid.AutoGeneratingColumn += _grid_AutoGeneratingColumn;
				}
			}

			if (e.OldValue != null)
			{
				AutoGenerateColumnCollection coll = e.OldValue as AutoGenerateColumnCollection;
				//coll.CollectionChanged -= AutoGenerateColumn_CollectionChanged;

				DataGrid grid = coll.Owner as DataGrid;
				if (grid != null)
				{
					grid.AutoGeneratingColumn -= _grid_AutoGeneratingColumn;
				}
			}
		}

		//static void AutoGenerateColumn_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		//{
		//    throw new NotImplementedException();
		//}
	}
}
