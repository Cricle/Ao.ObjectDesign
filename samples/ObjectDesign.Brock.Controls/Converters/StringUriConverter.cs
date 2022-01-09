using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.Converters
{
    public class StringUriConverter : IValueConverter
    {
        public static readonly StringUriConverter Instance = new StringUriConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && Uri.TryCreate(str, UriKind.Absolute, out var uri))
            {
                return uri;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }
}
