using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectDesign.Brock.Converters
{
    public class SettingNameConverter : IValueConverter
    {
        public static readonly SettingNameConverter Instance = new SettingNameConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.GetType().Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
