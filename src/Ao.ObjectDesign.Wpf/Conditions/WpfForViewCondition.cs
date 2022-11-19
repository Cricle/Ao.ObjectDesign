using Ao.ObjectDesign.ForView;
using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Conditions
{
    public abstract class WpfForViewCondition : IForViewCondition<FrameworkElement, WpfForViewBuildContext>
    {
        public virtual int Order { get; set; }

        public abstract bool CanBuild(WpfForViewBuildContext context);
        protected abstract FrameworkElement CreateView(WpfForViewBuildContext context);

        protected abstract void Bind(WpfForViewBuildContext context,
            FrameworkElement e,
            Binding binding);

        protected static Binding CreateWpfBinding(IPropertyProxy propertyProxy, BindingMode mode, UpdateSourceTrigger updateSourceTrigger, out DependencyProperty bindProperty)
        {
            System.Collections.Generic.IReadOnlyDictionary<string, System.ComponentModel.DependencyPropertyDescriptor> descriptorMap = DependencyObjectHelper.GetDependencyPropertyDescriptors(propertyProxy.DeclaringInstance.GetType());
            bindProperty = descriptorMap[propertyProxy.PropertyInfo.Name].DependencyProperty;
            Binding binding = new Binding(propertyProxy.PropertyInfo.Name)
            {
                Source = propertyProxy.DeclaringInstance,
                Mode = bindProperty.ReadOnly ? BindingMode.OneWay : mode,
                UpdateSourceTrigger = updateSourceTrigger,
            };
            return binding;
        }
        protected static Binding CreateClrBinding(WpfForViewBuildContext context)
        {
            Binding binding = new Binding(context.PropertyProxy.PropertyInfo.Name)
            {
                Source = context.PropertyProxy.DeclaringInstance,
                Mode = context.BindingMode,
                UpdateSourceTrigger = context.UpdateSourceTrigger,
            };
            return binding;
        }
        protected static bool IsDependencyProperty(object instance, string propertyName)
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
            FrameworkElement view = CreateView(context);
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
                    out DependencyProperty prop);
                view.IsEnabled = !prop.ReadOnly;
            }
            else
            {
                binding = CreateClrBinding(context);
            }
            Bind(context, view, binding);
            return view;
        }
    }
}
