using Ao.ObjectDesign;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectDesign.Wpf.Converters
{
    public class PropertyProxyNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IPropertyProxy proxy)
            {
                return proxy.PropertyInfo.Name;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
