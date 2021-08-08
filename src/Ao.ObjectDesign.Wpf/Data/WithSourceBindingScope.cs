using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using LExpressions = System.Linq.Expressions;

namespace Ao.ObjectDesign.Wpf.Data
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
        }

        public object Source { get; }

        public IBindingScope Scope { get; }

        public BindingExpressionBase Bind(DependencyObject @object)
        {
            return Scope.Bind(@object, Source);
        }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return Scope.Bind(@object, source);
        }
    }
}
