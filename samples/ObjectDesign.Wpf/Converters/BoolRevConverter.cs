using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ObjectDesign.Wpf.Converters
{
    public class BoolRevConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertCore(value);
        }
        private object ConvertCore(object value)
        {
            if (value is bool b)
            {
                return !b;
            }
            return Binding.DoNothing;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertCore(value);
        }
    }
}
