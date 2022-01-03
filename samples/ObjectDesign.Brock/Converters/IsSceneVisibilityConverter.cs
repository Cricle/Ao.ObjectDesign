using Ao.ObjectDesign.Designing.Level;
using ObjectDesign.Brock.Components;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ObjectDesign.Brock.Converters
{
    public class IsSceneVisibilityConverter : IValueConverter
    {
        public static readonly IsSceneVisibilityConverter Instance = new IsSceneVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is IDesignScene<UIElementSetting> ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

    }
}
