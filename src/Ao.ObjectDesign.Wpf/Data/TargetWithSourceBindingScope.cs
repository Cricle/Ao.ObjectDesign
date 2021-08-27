using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public abstract class TargetWithSourceBindingScope : IWithSourceBindingScope
    {
        protected TargetWithSourceBindingScope(object source, IBindingScope scope)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }

        public object Source { get; }

        public IBindingScope Scope { get; }

        public BindingExpressionBase Bind(DependencyObject @object)
        {
            var target = GetTargetValue();
            return Bind(@object, target);
        }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return Scope.Bind(@object, source);
        }

        public BindingBase CreateBinding(object source)
        {
            return Scope.CreateBinding(source);
        }

        protected abstract object GetTargetValue();
    }
}
