using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace DataGridTry
{
    public class ResourceSatellite : ViewModelBase
    {
        public ResourceSatellite(string resType, int minVal, int maxVal)
        {
            _resourceType = resType;
            _minValue = minVal;
            _maxValue = maxVal;
        }

        private string _resourceType;

        public string ResourceType
        {
            get { return _resourceType; }
            set { Set(() => ResourceType, ref _resourceType, value); }
        }

        private int _minValue;

        public int MinValue
        {
            get { return _minValue; }
            set { Set(() => MinValue, ref _minValue, value); }
        }

        private int _maxValue;

        public int MaxValue
        {
            get { return _maxValue; }
            set { Set(() => MaxValue, ref _maxValue, value); }
        }

        private ObservableCollection<ResourceSystem> _resourceSystems;

        public ObservableCollection<ResourceSystem> ResourceSystems
        {
            get { return _resourceSystems ?? (_resourceSystems = new ObservableCollection<ResourceSystem>()); }
            set { Set(() => ResourceSystems, ref _resourceSystems, value); }
        }


    }
}
