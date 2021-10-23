using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp1.ValueConverters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (targetType == typeof(Brush) && value is Color color) ?
            new SolidColorBrush(color) :
            value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
