using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IWithSourceBindingScope : IBindingScope
    {
        BindingExpressionBase Bind(DependencyObject @object);
    }
}
