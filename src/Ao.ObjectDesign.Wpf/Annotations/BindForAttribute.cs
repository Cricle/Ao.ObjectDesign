using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class BindForAttribute : Attribute
    {
        private static readonly Type DependencyObjectTypeValue = typeof(DependencyObject);
        private static readonly string IValueConverterName = typeof(IValueConverter).FullName;


        public BindForAttribute(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"“{nameof(propertyName)}”不能为 null 或空。", nameof(propertyName));
            }

            PropertyName = propertyName;
        }

        public BindForAttribute(Type dependencyObjectType, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"“{nameof(propertyName)}”不能为 null 或空。", nameof(propertyName));
            }

            DependencyObjectType = dependencyObjectType ?? throw new ArgumentNullException(nameof(dependencyObjectType));
            if (!DependencyObjectTypeValue.IsAssignableFrom(dependencyObjectType))
            {
                throw new ArgumentException($"Type {dependencyObjectType} is not base on DependencyObject");
            }
            PropertyName = propertyName;
        }
        private Type converterType;

        public Type DependencyObjectType { get; }

        public Type ConverterType
        {
            get => converterType;
            set
            {
                if (value != null && value.GetInterface(IValueConverterName) == null)
                {
                    throw new ArgumentException("ConverterType must implement interface IValueConverter");
                }
                converterType = value;
            }
        }

        public object ConverterParamer { get; set; }

        public string PropertyName { get; }
    }
}
