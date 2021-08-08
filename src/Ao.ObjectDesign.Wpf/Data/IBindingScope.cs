using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IBindingScope
    {
        BindingExpressionBase Bind(DependencyObject @object, object source);
    }
}
