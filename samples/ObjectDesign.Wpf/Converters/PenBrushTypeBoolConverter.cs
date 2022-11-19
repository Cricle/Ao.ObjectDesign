using Ao.ObjectDesign.Designing;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ObjectDesign.Wpf.Converters
{
    public class PenBrushTypeVisibilityConverter : IValueConverter
    {
        public static readonly PenBrushTypeVisibilityConverter Instance = new PenBrushTypeVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PenBrushTypes input && parameter is PenBrushTypes target)
            {
                return input == target ? Visibility.Visible : Visibility.Collapsed;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is PenBrushTypes type && value is Visibility v && v == Visibility.Visible)
            {
                return type;
            }
            return Binding.DoNothing;
        }
    }
    public class PenBrushTypeBoolConverter : IValueConverter
    {
        public static readonly PenBrushTypeBoolConverter Instance = new PenBrushTypeBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PenBrushTypes input && parameter is PenBrushTypes target)
            {
                return input == target;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is PenBrushTypes type && value is bool b && b)
            {
                return type;
            }
            return Binding.DoNothing;
        }
    }
}
