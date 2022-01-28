using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IBindingScope
    {
        DependencyProperty DependencyProperty { get; }

        BindingBase CreateBinding(object source);

        BindingExpressionBase Bind(DependencyObject @object, object source);
    }
}
