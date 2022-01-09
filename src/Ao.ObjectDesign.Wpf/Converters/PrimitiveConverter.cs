using System;
using System.Globalization;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Converters
{
    public class PrimitiveConverter : IValueConverter
    {
        public static readonly PrimitiveConverter Instance = new PrimitiveConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || targetType.IsInstanceOfType(value))
            {
                return value;
            }
            return TypeHelper.ChangeType(value, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || targetType.IsInstanceOfType(value))
            {
                return value;
            }
            return TypeHelper.ChangeType(value, targetType);
        }
    }
}
