using Ao.ObjectDesign.Designing.Level;
using ObjectDesign.Brock.Components;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectDesign.Brock.Converters
{
    public class DesigningCountConverter : IValueConverter
    {
        public static readonly DesigningCountConverter Instance = new DesigningCountConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IDesignScene<UIElementSetting> scene)
            {
                return scene.DesigningObjects.Count;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
