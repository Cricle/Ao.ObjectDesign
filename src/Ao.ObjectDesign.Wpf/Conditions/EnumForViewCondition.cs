using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;

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
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var convert = TypeDescriptor.GetConverter((Type)parameter);
                return convert.ConvertToString(value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var convert = TypeDescriptor.GetConverter((Type)parameter);
                return convert.ConvertFrom(value);
            }
        }

        protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
        {
            binding.Converter = new EnumValueConverter();
            binding.ConverterParameter = context.PropertyProxy.Type;

            e.SetBinding(ComboBox.SelectedItemProperty, binding);
        }
        protected override FrameworkElement CreateView(WpfForViewBuildContext context)
        {
            return new ComboBox { ItemsSource=Enum.GetNames(context.PropertyProxy.Type)};
        }
    }
}
