using Ao.ObjectDesign.Wpf.Data;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign.Test
{
    class NullWithSourceBindingScope : IWithSourceBindingScope
    {
        public object Source => null;

        public BindingExpressionBase Bind(DependencyObject @object)
        {
            return null;
        }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            return null;
        }

        public BindingBase CreateBinding(object source)
        {
            return null;
        }
    }
}
