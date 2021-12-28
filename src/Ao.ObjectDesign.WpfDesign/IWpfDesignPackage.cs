using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWpfDesignPackage<TDesignObject>: IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope>
    {
    }
}
