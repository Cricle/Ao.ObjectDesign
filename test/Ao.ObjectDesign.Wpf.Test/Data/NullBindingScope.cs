using Ao.ObjectDesign.Wpf.Data;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Test.Data
{
    class NullBindingScope : IBindingScope
    {
        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return null;
        }
    }
    class ValueBindingScope : IBindingScope
    {
        public bool IsBind { get; set; }
        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            IsBind = true;
            return null;
        }
    }
}
