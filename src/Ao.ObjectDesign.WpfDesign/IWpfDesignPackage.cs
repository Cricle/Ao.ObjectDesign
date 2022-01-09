using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Wpf.Data;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWpfDesignPackage<TDesignObject> : IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope>
    {
    }
}
