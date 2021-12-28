using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWpfBindingCreatorFactory<TDesignObject> : IBindingCreatorFactory<UIElement, TDesignObject, IWithSourceBindingScope>
    {
    }
}
