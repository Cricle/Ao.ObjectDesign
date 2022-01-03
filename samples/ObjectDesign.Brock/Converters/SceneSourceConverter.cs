using Ao.ObjectDesign.Designing.Level;
using ObjectDesign.Brock.Components;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ObjectDesign.Brock.Converters
{
    public class StringVisiblityConverter : IValueConverter
    {
        public static readonly StringVisiblityConverter Instance = new StringVisiblityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ok = value != null && !string.Empty.Equals(value.ToString());
            if (parameter?.ToString() == "rev")
            {
                ok = !ok;
            }
            return ok ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
    public class SceneSourceConverter : IValueConverter
    {
        public static readonly SceneSourceConverter Instance = new SceneSourceConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IDesignScene<UIElementSetting> scene)
            {
                return scene.DesigningObjects;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
