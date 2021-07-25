using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false,Inherited =false)]
    public sealed class BindForAdditionalAttribute : Attribute
    {
        public BindForAdditionalAttribute(string dependencyPropertyName)
        {
            if (string.IsNullOrEmpty(dependencyPropertyName))
            {
                throw new ArgumentException($"“{nameof(dependencyPropertyName)}”不能为 null 或空。", nameof(dependencyPropertyName));
            }

            DependencyPropertyName = dependencyPropertyName;
        }

        public BindForAdditionalAttribute(Type dependencyObjectType, string dependencyPropertyName)
        {
            if (string.IsNullOrEmpty(dependencyPropertyName))
            {
                throw new ArgumentException($"“{nameof(dependencyPropertyName)}”不能为 null 或空。", nameof(dependencyPropertyName));
            }

            DependencyObjectType = dependencyObjectType ?? throw new ArgumentNullException(nameof(dependencyObjectType));
            DependencyPropertyName = dependencyPropertyName;
        }

        public Type DependencyObjectType { get; }

        public string DependencyPropertyName { get; }
    }
}
