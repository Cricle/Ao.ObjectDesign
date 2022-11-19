using Ao.ObjectDesign.Data;
using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Test.Data
{
    class NullBindingScope : IBindingScope
    {
        public DependencyProperty DependencyProperty => null;

        public Func<DependencyObject> TargetFactory {get; set;}

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return null;
        }

        public BindingBase CreateBinding(object source)
        {
            return null;
        }
    }
    class ValueBindingScope : IBindingScope
    {
        public bool IsBind { get; set; }

        public DependencyProperty DependencyProperty => null;

        public Func<DependencyObject> TargetFactory { get; set; }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            IsBind = true;
            return null;
        }

        public BindingBase CreateBinding(object source)
        {
            return new Binding { Source = source };
        }
    }
}
