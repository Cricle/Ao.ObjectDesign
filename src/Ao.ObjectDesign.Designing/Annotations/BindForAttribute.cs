using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class BindForAttribute : Attribute
    {
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
            PropertyName = propertyName;
        }

        public Type DependencyObjectType { get; }

        public virtual Type ConverterType { get; set; }

        public object ConverterParamer { get; set; }

        public string VisitPath { get; set; }

        public string PropertyName { get; }

    }
}
