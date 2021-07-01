using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Conditions
{
    public class ValueTypeForViewCondition : WpfForViewCondition
    {
        public override int Order { get; set; } = -1;

        public override bool CanBuild(WpfForViewBuildContext context)
        {
            return context.PropertyProxy.Type.IsPrimitive ||
                context.PropertyProxy.Type == typeof(string) ||
                context.PropertyProxy.Type.IsGenericType && context.PropertyProxy.Type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        protected override FrameworkElement CreateView(WpfForViewBuildContext context)
        {
            return new TextBox();
        }
        protected override void Bind(WpfForViewBuildContext context, FrameworkElement e, Binding binding)
        {
            e.SetBinding(TextBox.TextProperty, binding);
        }
    }
}
