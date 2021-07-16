using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Converters
{
    public class StringFontFamilyConverter : IValueConverter
    {
        public static readonly StringFontFamilyConverter Instance = new StringFontFamilyConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && FontFamilyDesigner.InstalledFontFamilyMap.TryGetValue(str, out FontFamily f))
            {
                return f;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FontFamily ff)
            {
                return ff.Source;
            }
            return null;
        }
    }
}
