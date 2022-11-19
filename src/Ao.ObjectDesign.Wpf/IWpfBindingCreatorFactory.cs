using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Data;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfBindingCreatorFactory<TDesignObject> : IBindingCreatorFactory<UIElement, TDesignObject, IWithSourceBindingScope>
    {
    }
}
