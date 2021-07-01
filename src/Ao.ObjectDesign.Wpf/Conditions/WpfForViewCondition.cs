using Ao.ObjectDesign.ForView;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Conditions
{
    public abstract class WpfForViewCondition : IForViewCondition<FrameworkElement, WpfForViewBuildContext>
    {
        public virtual int Order { get; set; }

        public abstract bool CanBuild(WpfForViewBuildContext context);
        protected abstract FrameworkElement CreateView(WpfForViewBuildContext context);

        protected abstract void Bind(WpfForViewBuildContext context,
            FrameworkElement e,
            Binding binding);

        public FrameworkElement Create(WpfForViewBuildContext context)
        {
            var view = CreateView(context);
            view.IsEnabled = context.PropertyProxy.PropertyInfo.CanWrite;
            if (context.PropertyProxy.DeclaringInstance is DependencyObject &&
                DependencyObjectHelper.IsDependencyProperty(context.PropertyProxy.DeclaringInstance.GetType(), context.PropertyProxy.PropertyInfo.Name))
            {
                var prop = DependencyObjectHelper.GetDependencyProperties(context.PropertyProxy.DeclaringInstance.GetType())
                    .First(x => x.Name == context.PropertyProxy.PropertyInfo.Name);
                view.IsEnabled = !prop.ReadOnly;
                var binding = new Binding(context.PropertyProxy.PropertyInfo.Name)
                {
                    Source = context.PropertyProxy.DeclaringInstance,
                    Mode = prop.ReadOnly? BindingMode.OneWay: context.BindingMode,
                    UpdateSourceTrigger = context.UpdateSourceTrigger,
                };
                Bind(context,view, binding);
            }
            else
            {
                var visitor = context.GetPropertyVisitor();
                var binding = new Binding(nameof(PropertyVisitor.Value))
                {
                    Source = visitor,
                    Mode = context.BindingMode,
                    UpdateSourceTrigger = context.UpdateSourceTrigger,
                };
                Bind(context, view, binding);
            }
            return view;
        }
    }
}
