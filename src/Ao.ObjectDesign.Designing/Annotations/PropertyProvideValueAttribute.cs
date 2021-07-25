using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class PropertyProvideValueAttribute : Attribute
    {
        public PropertyProvideValueAttribute(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"“{nameof(propertyName)}”不能为 null 或空。", nameof(propertyName));
            }

            PropertyName = propertyName;
        }
        public PropertyProvideValueAttribute(string propertyName, Type provideType)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"“{nameof(propertyName)}”不能为 null 或空。", nameof(propertyName));
            }

            PropertyName = propertyName;
            ProvideType = provideType ?? throw new ArgumentNullException(nameof(provideType));
        }

        public string PropertyName { get; }

        public Type ProvideType { get; }
    }
}
