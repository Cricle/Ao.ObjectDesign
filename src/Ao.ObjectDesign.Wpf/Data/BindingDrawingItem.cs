using System;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Data
{
    internal class BindingDrawingItem : IBindingDrawingItem
    {
        public Type ClrType { get; set; }

        public Type DependencyObjectType { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public DependencyProperty DependencyProperty { get; set; }

        public string Path { get; set; }

        public bool HasPropertyBind { get; set; }

        public override string ToString()
        {
            return $"{{Property:{PropertyInfo.Name}, DependencyProperty:{DependencyProperty.Name}, Path:{Path}, HasBind:{HasPropertyBind}}}";
        }
    }
}
