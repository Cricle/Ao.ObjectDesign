using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Bindings;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWpfBindingScopeCreator: IBindingScopeCreator<UIElement,BindingExpressionBase,DependencyObject>
    {
    }
}
