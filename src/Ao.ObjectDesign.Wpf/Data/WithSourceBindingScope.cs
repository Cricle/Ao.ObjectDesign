using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    public class WithSourceBindingScope : IWithSourceBindingScope
    {
        public WithSourceBindingScope(IBindingScope scope)
            : this(scope, null)
        {

        }
        public WithSourceBindingScope(IBindingScope scope, object source)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Source = source;
            DependencyProperty=scope.DependencyProperty;
        }

        public object Source { get; }

        public IBindingScope Scope { get; }

        public DependencyProperty DependencyProperty { get; }

        public Func<DependencyObject> TargetFactory { get; set; }

        public BindingExpressionBase Bind(DependencyObject @object)
        {
            var target = TargetFactory?.Invoke() ?? @object;
            return Scope.Bind(target, Source);
        }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            var target = TargetFactory?.Invoke() ?? @object;
            return Scope.Bind(target, source);
        }

        public BindingBase CreateBinding(object source)
        {
            return Scope.CreateBinding(source);
        }
    }
}
