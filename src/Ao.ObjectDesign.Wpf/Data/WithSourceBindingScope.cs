﻿using System;
using System.Windows;
using System.Windows.Data;

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
            DependencyProperty=scope.DependencyProperty;
        }

        public object Source { get; }

        public IBindingScope Scope { get; }

        public DependencyProperty DependencyProperty { get; }

        public BindingExpressionBase Bind(DependencyObject @object)
        {
            return Scope.Bind(@object, Source);
        }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return Scope.Bind(@object, source);
        }

        public BindingBase CreateBinding(object source)
        {
            return Scope.CreateBinding(source);
        }
    }
}
