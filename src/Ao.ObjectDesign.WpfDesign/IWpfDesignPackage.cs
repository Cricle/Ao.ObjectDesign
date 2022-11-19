using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Data;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWpfDesignPackage<TDesignObject> : IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope>
    {
    }
}
