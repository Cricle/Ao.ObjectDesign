using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Wpf.Data;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWpfBindingCreator<TDesignObject> : IBindingCreator<UIElement, TDesignObject, IWithSourceBindingScope>
    {
    }
}
