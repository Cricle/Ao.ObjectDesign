using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectDesign.Brock.Converters
{
    public abstract class EnumBoolConverter<TEnum> : IValueConverter
        where TEnum : unmanaged
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TEnum val && parameter is TEnum dest)
            {
                return val.Equals(dest);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val && val)
            {
                return parameter;
            }
            return Binding.DoNothing;
        }
    }
}
