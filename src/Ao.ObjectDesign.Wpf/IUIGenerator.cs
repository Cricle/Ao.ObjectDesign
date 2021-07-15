using System.Collections.Generic;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Data;
using Ao.ObjectDesign.ForView;

namespace Ao.ObjectDesign.Wpf
{
    public interface IUIGenerator<TView, TContext>
    {
        IEnumerable<IUISpirit<TView, TContext>> Generate(IEnumerable<IPropertyProxy> propertyProxies);
    }
}
