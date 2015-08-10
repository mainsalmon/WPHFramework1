using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WPHFramework1
{
    public class FlipBooleanConverter : IValueConverter
    {
        // Reverse the value of boolean passed in
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool x = Boolean.Parse(value.ToString());
            return !x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool x = Boolean.Parse(value.ToString());
            return !x;
        }
    }

}
