using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    public abstract class TargetWithSourceBindingScope : IWithSourceBindingScope
    {
        protected TargetWithSourceBindingScope(object source, IBindingScope scope)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            DependencyProperty = scope.DependencyProperty;
        }

        public object Source { get; }

        public IBindingScope Scope { get; }

        public DependencyProperty DependencyProperty { get; }

        public Func<DependencyObject> TargetFactory { get; set; }

        public BindingExpressionBase Bind(DependencyObject @object)
        {
            var target = GetTargetValue();
            return Bind(TargetFactory?.Invoke()??@object, target);
        }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return Scope.Bind(TargetFactory?.Invoke() ?? @object, source);
        }

        public BindingBase CreateBinding(object source)
        {
            return Scope.CreateBinding(source);
        }

        protected abstract object GetTargetValue();
    }
}
