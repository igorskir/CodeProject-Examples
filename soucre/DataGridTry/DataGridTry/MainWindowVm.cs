using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight;

namespace DataGridTry
{
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

    public class MainWindowVm : ViewModelBase
    {
        public MainWindowVm()
        {
            ResourceSatellites.Add(new ResourceSatellite("Тип 1", 10, 20)
            {
                ResourceSystems = new ObservableCollection<ResourceSystem>()
                {
                    new ResourceSystem("Система 1", 10, 13), 
                    new ResourceSystem("Система 2", 13, 16), 
                    new ResourceSystem("Система 3", 16, 20)
                }
            });

            ResourceSatellites.Add(new ResourceSatellite("Тип 2", 777, 999)
            {
                ResourceSystems = new ObservableCollection<ResourceSystem>()
                {
                    new ResourceSystem("Система 1", 777, 888), 
                    new ResourceSystem("Система 3", 889, 999)
                }
            });

            ColumnCollection.Add(new DataGridTextColumn() { Header = "Тип ресурса", Binding = new Binding("ResourceType")});
            ColumnCollection.Add(new DataGridTextColumn() { Header = "Мин", Binding = new Binding("MinValue")});
            ColumnCollection.Add(new DataGridTextColumn() { Header = "Макс", Binding = new Binding("MaxValue")});

            //var systList = new List<ResourceSystem> { "Система 1", "Система 2", "Система 3", "Система 4" };

            //foreach (var syst in systList)
            //{
            //    ColumnCollection.Add(new DataGridTextColumn() 
            //    { Header = syst, Binding = new Binding("ResourceSystems") 
            //        {Converter = new SystemConverter(), ConverterParameter = syst} 
            //    });
            //}
        }

        private ObservableCollection<ResourceSatellite> _resourceSatellites;

        public ObservableCollection<ResourceSatellite> ResourceSatellites
        {
            get { return _resourceSatellites ?? (_resourceSatellites = new ObservableCollection<ResourceSatellite>()); }
        }

        private ObservableCollection<DataGridTextColumn> _columnCollection;

        public ObservableCollection<DataGridTextColumn> ColumnCollection
        {
            get { return _columnCollection ?? (_columnCollection = new ObservableCollection<DataGridTextColumn>()); }
            set { Set(() => ColumnCollection, ref _columnCollection, value); }
        }
    }
}
