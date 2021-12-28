using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWpfBindingCreator<TDesignObject>: IBindingCreator<UIElement,TDesignObject, IWithSourceBindingScope>
    {
    }
}
