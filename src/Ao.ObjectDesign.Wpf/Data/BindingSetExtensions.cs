using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Data
{
    public static class BindingSetExtensions
    {
        public static void SetBindings(this FrameworkElement element, IEnumerable<BindingUnit> bindings)
        {
            foreach (BindingUnit item in bindings)
            {
                element.SetBinding(item.DependencyProperty, item.Binding);
            }
        }
        public static void SetBindings(this FrameworkContentElement element, IEnumerable<BindingUnit> bindings)
        {
            foreach (BindingUnit item in bindings)
            {
                element.SetBinding(item.DependencyProperty, item.Binding);
            }
        }
    }
}
