using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    public class PropertyIdentity : IEquatable<PropertyIdentity>
    {
        public PropertyIdentity(Type type, string propertyName)
        {
            Type = type;
            PropertyName = propertyName;
        }

        public Type Type { get; }

        public string PropertyName { get; }

        public bool Equals(PropertyIdentity other)
        {
            if (other is null)
            {
                return false;
            }
            return other.Type == Type &&
                other.PropertyName == PropertyName;
        }
        public override string ToString()
        {
            return $"{{{Type}, {PropertyName}}}";
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as PropertyIdentity);
        }
        public override int GetHashCode()
        {
#if NET5_0
            return HashCode.Combine(Type,PropertyName);
#else
            return Type.GetHashCode() ^ PropertyName.GetHashCode();
#endif
        }
    }
}
