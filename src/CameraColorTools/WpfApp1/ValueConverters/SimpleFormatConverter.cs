using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.ValueConverters
{
    public class SimpleFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value switch
            {
                double input => input.ToString("000"),
                _ => value.ToString()
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
