using System;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IBindingDrawingItem
    {
        Type ClrType { get; }

        Type DependencyObjectType { get; }

        PropertyInfo PropertyInfo { get; }

        DependencyProperty DependencyProperty { get; }

        string Path { get; }

        Type ConverterType { get; }

        object ConverterParamter { get; }

        bool HasPropertyBind { get; }

    }
}
