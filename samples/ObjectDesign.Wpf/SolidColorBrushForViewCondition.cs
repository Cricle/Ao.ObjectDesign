using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace ObjectDesign.Wpf
{
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
            ColorPicker picker = new ColorPicker();
            Popup pop = new Popup { StaysOpen = false };
            pop.Child = picker;
            Button button = new Button { MaxWidth = 200, HorizontalAlignment = HorizontalAlignment.Left };
            button.Content = pop;
            SolidColorBrush brush = new SolidColorBrush();
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
