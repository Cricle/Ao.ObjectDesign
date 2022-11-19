using Ao.ObjectDesign.ForView;
using System;
using System.Windows;

namespace Ao.ObjectDesign.Conditions
{
    public abstract class TemplateCondition<T> : TemplateCondition
    {
        private readonly Type targetType = typeof(T);

        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type == targetType;
        }
    }
    public abstract class TemplateCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public virtual int Order { get; set; }

        public abstract bool CanBuild(WpfTemplateForViewBuildContext context);
        protected abstract string GetResourceKey();
        public virtual DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            string key = GetResourceKey();
            return (DataTemplate)Application.Current.FindResource(key);
        }
    }
}
