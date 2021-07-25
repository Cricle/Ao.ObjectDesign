using System;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.Designing.Data
{
    public interface IBindingDrawingItem
    {
        Type ClrType { get; }

        Type DependencyObjectType { get; }

        PropertyInfo PropertyInfo { get; }

        string Path { get; }

        Type ConverterType { get; }

        object ConverterParamter { get; }

        bool HasPropertyBind { get; }

    }
}
