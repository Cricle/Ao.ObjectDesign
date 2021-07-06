using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using Ao.ObjectDesign.Wpf.Conditions;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Globalization;
using Ao.ObjectDesign.Controls;

namespace ObjectDesign.Wpf
{
    public class DateTimeBrushForViewCondition : WpfForViewCondition
    {
        public override bool CanBuild(WpfForViewBuildContext context)
        {
            return context.PropertyProxy.Type == typeof(DateTime) ||
                context.PropertyProxy.Type == typeof(DateTime?);
        }

        protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
        {
            e.SetBinding(DateTimePicker.SelectedDateTimeProperty, binding);
        }

        protected override FrameworkElement CreateView(WpfForViewBuildContext context)
        {
            return new DateTimePicker();
        }
    }
    public class SolidColorBrushForViewCondition : WpfForViewCondition
    {

        public override bool CanBuild(WpfForViewBuildContext context)
        {
            if (typeof(Brush).IsAssignableFrom(context.PropertyProxy.Type))
            {
                return context.PropertyVisitor.Value is SolidColorBrush;
            }
            return false;
        }
        protected override FrameworkElement CreateView(WpfForViewBuildContext context)
        {
            var picker = new ColorPicker();
            var pop = new Popup { StaysOpen = false };
            pop.Child = picker;
            var button = new Button { MaxWidth = 200, HorizontalAlignment = HorizontalAlignment.Left };
            button.Content = pop;
            var brush = new SolidColorBrush();
            button.Background = brush;
            button.SetBinding(Button.BackgroundProperty, new Binding($"{context.PropertyProxy.PropertyInfo.Name}")
            {
                Source = context.PropertyProxy.DeclaringInstance
            });
            button.Click += (_, __) => pop.IsOpen = true;
            return button;
        }

        protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
        {
            if (e is Button btn && btn.Content is Popup pop && pop.Child is ColorPicker picker)
            {
                binding.Path = new PropertyPath($"{context.PropertyProxy.PropertyInfo.Name}.Color");

                picker.SetBinding(ColorPicker.SelectedColorProperty, binding);
            }
        }
    }
}
