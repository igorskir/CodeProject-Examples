using System;
using System.Windows.Data;

namespace WPFTreeViewDemo
{
    public class DateToTextConverter:IValueConverter
    {
        public static DateToTextConverter Instance = new DateToTextConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            return ((DateTime)value).ToString((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
