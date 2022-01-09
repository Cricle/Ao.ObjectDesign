using System;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public readonly struct PropertyIdentity : IEquatable<PropertyIdentity>
    {
        public static readonly PropertyIdentity Empty = new PropertyIdentity();

        public PropertyIdentity(PropertyIdentity identity)
        {
            Type = identity.Type;
            PropertyName = identity.PropertyName;
            PropertyInfo = identity.PropertyInfo;
        }
        public PropertyIdentity(PropertyInfo info)
        {
            PropertyInfo = info ?? throw new ArgumentNullException(nameof(info));
            Type = info.DeclaringType;
            PropertyName = info.Name;
        }
        public PropertyIdentity(Type type, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"“{nameof(propertyName)}”不能为 null 或空。", nameof(propertyName));
            }

            Type = type ?? throw new ArgumentNullException(nameof(type));
            PropertyName = propertyName;
            PropertyInfo = type.GetProperty(propertyName);
            if (PropertyInfo is null)
            {
                throw new ArgumentException($"Type {type} can't found property {PropertyName}");
            }
        }

        public readonly Type Type;

        public readonly string PropertyName;

        public readonly PropertyInfo PropertyInfo;

        public bool Equals(PropertyIdentity other)
        {
            return other.Type == Type &&
                other.PropertyName == PropertyName;
        }
        public override string ToString()
        {
            return $"{{{Type}, {PropertyName}}}";
        }
        public override bool Equals(object obj)
        {
            if (obj is PropertyIdentity)
            {
                return Equals((PropertyIdentity)obj);
            }
            return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                var h = 0;
                if (Type != null)
                {
                    h = Type.GetHashCode();
                }
                hash = hash * 31 + h;
                if (PropertyName != null)
                {
                    h = PropertyName.GetHashCode();
                }
                return hash * 31 + h;
            }
        }
        public static bool operator ==(PropertyIdentity a, PropertyIdentity b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(PropertyIdentity a, PropertyIdentity b)
        {
            return !a.Equals(b);
        }
    }
}
