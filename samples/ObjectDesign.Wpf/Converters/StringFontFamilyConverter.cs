using Ao.ObjectDesign.Wpf.Designing;
using ObjectDesign.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ObjectDesign.Wpf.Converters
{
    public class StringFontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && FontFamilyDesigner.InstalledFontFamilyMap.TryGetValue(str, out var f))
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
