using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using System;
using System.Windows;

namespace ObjectDesign.Wpf.Views
{
    public class FontFamilyCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public int Order { get; set; }

        public bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type == typeof(FontFamilyDesigner);
        }

        public DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            return (DataTemplate)Application.Current.FindResource("ObjectDesign.FontFamily");
        }
    }
    public class PrimitiveCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public int Order { get; set; }

        public bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type.IsPrimitive ||
                context.PropertyProxy.Type == typeof(string) ||
                (context.PropertyProxy.Type.IsGenericType && context.PropertyProxy.Type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            string selectKey = "ObjectDesign.Number";
            if (context.PropertyProxy.Type == typeof(string))
            {
                selectKey = "ObjectDesign.Primitive";
            }
            else if (context.PropertyProxy.Type == typeof(bool) ||
                context.PropertyProxy.Type == typeof(bool?))
            {
                selectKey = "ObjectDesign.Boolean";
            }
            return (DataTemplate)Application.Current.FindResource(selectKey);
        }
    }
}