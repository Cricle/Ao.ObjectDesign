using System;

namespace Ao.ObjectDesign.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class PropertyProvideValueAttribute : Attribute
    {
        public PropertyProvideValueAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
        public PropertyProvideValueAttribute(string propertyName, Type provideType)
        {
            PropertyName = propertyName;
            ProvideType = provideType;
        }

        public string PropertyName { get; }

        public Type ProvideType { get; }
    }
}
