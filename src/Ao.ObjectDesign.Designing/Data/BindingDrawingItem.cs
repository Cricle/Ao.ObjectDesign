using System;
using System.Reflection;

namespace Ao.ObjectDesign.Designing.Data
{
    public class BindingDrawingItem : IBindingDrawingItem
    {
        public Type ClrType { get; set; }

        public Type DependencyObjectType { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public string Path { get; set; }

        public bool HasPropertyBind { get; set; }

        public Type ConverterType { get; set; }

        public object ConverterParamter { get; set; }

        public override string ToString()
        {
            return $"{{Property:{PropertyInfo.Name}, Path:{Path}, HasBind:{HasPropertyBind}}}";
        }
    }
}
