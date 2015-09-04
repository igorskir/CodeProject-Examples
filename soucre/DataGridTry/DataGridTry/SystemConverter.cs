using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataGridTry
{
    public class SystemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resSystems = value as IEnumerable<ResourceSystem>;
            var resSyst = parameter as String;
            if (resSystems == null || resSyst == null) return Binding.DoNothing;

            var curSyst = resSystems.FirstOrDefault(r => r.SystemName == resSyst);
            return curSyst == null ? Binding.DoNothing : String.Format("{0}-{1}", curSyst.MinValue, curSyst.MaxValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
