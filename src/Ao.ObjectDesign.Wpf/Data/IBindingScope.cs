using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IBindingScope
    {
        BindingBase CreateBinding(object source);

        BindingExpressionBase Bind(DependencyObject @object, object source);
    }
}
