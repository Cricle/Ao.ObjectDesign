using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ObjectDesign.Wpf.Converters
{
    public class TypeEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type t)
            {
                var values = Enum.GetNames(t);
                return values;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str&& parameter is Type t)
            {
                try
                {
                    return Enum.Parse(t, str);
                }
                catch (Exception) { }
            }
            return Binding.DoNothing;
        }
    }
}
