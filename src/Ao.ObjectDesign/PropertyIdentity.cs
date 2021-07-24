using System;

namespace Ao.ObjectDesign
{
    public class PropertyIdentity : IEquatable<PropertyIdentity>
    {
        public PropertyIdentity(PropertyIdentity identity)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

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
            return Type.GetHashCode() ^ PropertyName.GetHashCode();
        }
    }
}
