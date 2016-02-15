using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataGridTry
{
    public class DataGridColumnBehavior
    {
        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached("BindableColumns", typeof(ObservableCollection<DataGridColumn>),
                typeof(DataGridColumnBehavior), new UIPropertyMetadata(null, BindableColumnsPropertyChanged));

        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = source as DataGrid;
            if (dataGrid == null) return;

            var columns = e.NewValue as ObservableCollection<DataGridColumn>;
            dataGrid.Columns.Clear();

            if (columns == null) return;          

            foreach (var column in columns)
            {
                dataGrid.Columns.Add(column);
            }

            columns.CollectionChanged += (sender, eArgs) =>
            {
                switch (eArgs.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        dataGrid.Columns.Clear();
                        if (eArgs.NewItems == null) return;
                        foreach (var column in columns)
                        {
                            dataGrid.Columns.Add(column);
                        }
                        break;
                    case NotifyCollectionChangedAction.Add:
                        if (eArgs.NewItems == null) return;
                        foreach (var column in columns)
                        {
                            dataGrid.Columns.Add(column);
                        }
                        break;
                    case NotifyCollectionChangedAction.Move:
                        dataGrid.Columns.Move(eArgs.OldStartingIndex, eArgs.NewStartingIndex);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        if (eArgs.OldItems == null) return;
                        foreach (DataGridColumn column in eArgs.OldItems)
                        {
                            dataGrid.Columns.Remove(column);
                        }
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        dataGrid.Columns[eArgs.NewStartingIndex] = eArgs.NewItems[0] as DataGridColumn;
                        break;
                }
            };
        }

        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }

        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>) element.GetValue(BindableColumnsProperty);
        }
    }
}
