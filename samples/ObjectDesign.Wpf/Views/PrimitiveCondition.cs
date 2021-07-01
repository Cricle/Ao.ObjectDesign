using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;

namespace ObjectDesign.Wpf.Views
{
    public class PrimitiveCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public int Order { get; set; }

        public bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type.IsPrimitive ||
                context.PropertyProxy.Type == typeof(string) ||
                context.PropertyProxy.Type.IsGenericType && context.PropertyProxy.Type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            var selectKey = "ObjectDesign.Number";
            if (context.PropertyProxy.Type == typeof(string))
            {
                selectKey = "ObjectDesign.Primitive";
            }
            return (DataTemplate)Application.Current.FindResource(selectKey);
        }
    }
}
