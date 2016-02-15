using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DataGridColumns.Annotations;

namespace DataGridColumns
{
    class MainWindowVM
    {
        public MainWindowVM()
        {
            var records = new ObservableCollection<Record>();
            records.Add(new Record(new Property("FirstName", "Paul"), new Property("LastName", "Stovell")));
            records.Add(new Record(new Property("FirstName", "Tony"), new Property("LastName", "Black")));

            var columns = records.First().Properties.Select((x, i) => new {Name = x.Name, Index = i}).ToArray();

            List<CustomBoundColumn> colList = new List<CustomBoundColumn>();

            foreach (var column in columns)
            {
                var binding = new Binding(string.Format("Properties[{0}].Value", column.Index));

                colList.Add(
                    new CustomBoundColumn
                    {
                        Header = column.Name,
                        Binding = binding,
                        TemplateName = "CustomTemplate"
                    });
            }

        }
    }

    public class Property : INotifyPropertyChanged
    {
        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Record
    {
        
        public Record(params Property[] properties)
        {
            foreach (var property in properties)
                Properties.Add(property);
        }

        private readonly ObservableCollection<Property> _properties = new ObservableCollection<Property>();

        public ObservableCollection<Property> Properties
        {
            get { return _properties; }
        }
    }

    public class CustomBoundColumn : DataGridBoundColumn
    {
        public string TemplateName { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var binding = new Binding(((Binding)Binding).Path.Path);
            binding.Source = dataItem;

            var content = new ContentControl();
            content.ContentTemplate = (DataTemplate)cell.FindResource(TemplateName);
            content.SetBinding(ContentControl.ContentProperty, binding);
            return content;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}
