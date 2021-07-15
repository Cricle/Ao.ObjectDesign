using Ao.ObjectDesign.ForView;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

        protected Binding CreateWpfBinding(IPropertyProxy propertyProxy, BindingMode mode, UpdateSourceTrigger updateSourceTrigger, out DependencyProperty bindProperty)
        {
            var descriptorMap = DependencyObjectHelper.GetDependencyPropertyDescriptors(propertyProxy.DeclaringInstance.GetType());
            bindProperty = descriptorMap[propertyProxy.PropertyInfo.Name].DependencyProperty;
            var binding = new Binding(propertyProxy.PropertyInfo.Name)
            {
                Source = propertyProxy.DeclaringInstance,
                Mode = bindProperty.ReadOnly ? BindingMode.OneWay : mode,
                UpdateSourceTrigger = updateSourceTrigger,
            };
            return binding;
        }
        protected Binding CreateClrBinding(IPropertyVisitor visitor, BindingMode mode, UpdateSourceTrigger updateSourceTrigger)
        {
            var binding = new Binding(nameof(PropertyVisitor.Value))
            {
                Source = visitor,
                Mode = mode,
                UpdateSourceTrigger = updateSourceTrigger,
            };
            return binding;
        }
        protected bool IsDependencyProperty(object instance, string propertyName)
        {
            if (instance is DependencyObject)
            {
                return IsDependencyProperty(instance.GetType(), propertyName);
            }
            return false;
        }
        protected bool IsDependencyProperty(Type type, string propertyName)
        {
            if (typeof(DependencyObject).IsAssignableFrom(type))
            {
                return DependencyObjectHelper.IsDependencyProperty(type, propertyName);
            }
            return false;
        }
        public FrameworkElement Create(WpfForViewBuildContext context)
        {
            var view = CreateView(context);
            if (view is null)
            {
                return null;
            }
            view.IsEnabled = context.PropertyProxy.PropertyInfo.CanWrite;
            Binding binding;
            if (IsDependencyProperty(context.PropertyProxy.DeclaringInstance, context.PropertyProxy.PropertyInfo.Name))
            {
                binding = CreateWpfBinding(context.PropertyProxy,
                    context.BindingMode,
                    context.UpdateSourceTrigger,
                    out var prop);
                view.IsEnabled = !prop.ReadOnly;
            }
            else
            {
                var visitor = context.PropertyVisitor;
                binding = CreateClrBinding(visitor, context.BindingMode, context.UpdateSourceTrigger);
            }
            Bind(context, view, binding);
            return view;
        }
    }
}
