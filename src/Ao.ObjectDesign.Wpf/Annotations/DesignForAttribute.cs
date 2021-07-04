using System;

namespace Ao.ObjectDesign.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Class,Inherited =false,AllowMultiple =true)]
    public sealed class DesignForAttribute : Attribute
    {
        public DesignForAttribute(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public Type Type { get; }
    }
}
