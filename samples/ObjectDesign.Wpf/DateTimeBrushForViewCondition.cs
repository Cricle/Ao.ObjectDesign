using Ao.ObjectDesign.Wpf;
using System;
using System.Windows;
using MahApps.Metro.Controls;
using Ao.ObjectDesign.Wpf.Conditions;
using System.Windows.Data;

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
}
