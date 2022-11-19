using Ao.ObjectDesign.Bindings;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfBindingScopeCreator : IBindingScopeCreator<UIElement, BindingExpressionBase, DependencyObject>
    {
    }
}
