using System;

namespace Ao.ObjectDesign
{
    public readonly struct PropertyIdentity : IEquatable<PropertyIdentity>
    {
        public static readonly PropertyIdentity Empty = new PropertyIdentity();

        public PropertyIdentity(PropertyIdentity identity)
        {
            Type = identity.Type;
            PropertyName = identity.PropertyName;
        }
        public PropertyIdentity(Type type, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"“{nameof(propertyName)}”不能为 null 或空。", nameof(propertyName));
            }

            Type = type ?? throw new ArgumentNullException(nameof(type));
            PropertyName = propertyName;
        }

        public Type Type { get; }

        public string PropertyName { get; }

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
            return (Type?.GetHashCode() ?? 0) ^ (PropertyName?.GetHashCode() ?? 0);
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
