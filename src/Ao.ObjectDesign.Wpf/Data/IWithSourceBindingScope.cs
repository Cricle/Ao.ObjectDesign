using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IWithSourceBindingScope : IBindingScope
    {
        object Source { get; }

        BindingExpressionBase Bind(DependencyObject @object);
    }
}
