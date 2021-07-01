using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Conditions
{
    public abstract class TemplateCondition<T> : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type == typeof(T);
        }
    }
    public abstract class TemplateCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public virtual int Order { get; set; }

        public abstract bool CanBuild(WpfTemplateForViewBuildContext context);
        protected abstract string GetResourceKey();
        public virtual DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            var key = GetResourceKey();
            return (DataTemplate)Application.Current.FindResource(key);
        }
    }
}
