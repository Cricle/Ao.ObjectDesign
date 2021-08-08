using Ao.ObjectDesign.Wpf.Converters;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Conditions
{
    public class EnumForViewCondition : WpfForViewCondition
    {
        public override bool CanBuild(WpfForViewBuildContext context)
        {
            return context.PropertyProxy.Type.IsEnum;
        }

        class EnumValueConverter : IValueConverter
        {
            public static readonly EnumValueConverter Instance = new EnumValueConverter();

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                TypeConverter convert = TypeDescriptor.GetConverter((Type)parameter);
                return convert.ConvertToString(value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                TypeConverter convert = TypeDescriptor.GetConverter((Type)parameter);
                return convert.ConvertFrom(value);
            }
        }

        protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
        {
            binding.Converter = EnumValueConverter.Instance;
            binding.ConverterParameter = context.PropertyProxy.Type;

            e.SetBinding(ComboBox.SelectedItemProperty, binding);
        }
        protected override FrameworkElement CreateView(WpfForViewBuildContext context)
        {
            return new ComboBox { ItemsSource = Enum.GetNames(context.PropertyProxy.Type) };
        }
    }
}
