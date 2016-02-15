using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace DataGridTry
{
    public class ResourceSystem : ViewModelBase
    {
        public ResourceSystem(string systName, int minVal, int maxVal)
        {
            _systemName = systName;
            _minValue = minVal;
            _maxValue = maxVal;
        }

        private string _systemName;

        public string SystemName
        {
            get { return _systemName; }
            set { Set(() => SystemName, ref _systemName, value); }
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
    }
}
