using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ObjectDesign.Brock.Converters
{
    public abstract class EnumVisibilityConverter<TEnum> : IValueConverter
       where TEnum : unmanaged
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TEnum input && parameter is TEnum target)
            {
                return input.Equals(target) ? Visibility.Visible : Visibility.Collapsed;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is TEnum type && value is Visibility v && v == Visibility.Visible)
            {
                return type;
            }
            return Binding.DoNothing;
        }
    }
}
