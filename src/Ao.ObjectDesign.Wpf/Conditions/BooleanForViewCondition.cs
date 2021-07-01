using Ao.ObjectDesign.ForView;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Conditions
{
    public class BooleanForViewCondition : WpfForViewCondition
    {
        public override bool CanBuild(WpfForViewBuildContext context)
        {
            return context.PropertyProxy.Type == typeof(bool) ||
                context.PropertyProxy.Type == typeof(bool?);
        }
        protected override FrameworkElement CreateView(WpfForViewBuildContext context)
        {
            return new CheckBox { IsEnabled = context.PropertyProxy.PropertyInfo.CanWrite };
        }
        protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
        {
            e.SetBinding(CheckBox.IsCheckedProperty, binding);
        }
    }
}
