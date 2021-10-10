using System;
using System.Diagnostics;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public class PropertyDeclare : ObjectDeclaring, IPropertyDeclare,IEquatable<PropertyDeclare>
    {
        public PropertyDeclare(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        public PropertyInfo PropertyInfo { get; }

        public override Type Type => PropertyInfo.PropertyType;

        public override int GetHashCode()
        {
            Debug.Assert(PropertyInfo != null);

            return PropertyInfo.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is PropertyDeclare)
            {
                return Equals((PropertyDeclare)obj);
            }
            return false;
        }

        public bool Equals(PropertyDeclare other)
        {
            if (other is null)
            {
                return false;
            }
            return other.PropertyInfo == PropertyInfo;
        }
        public override string ToString()
        {
            return $"{{{PropertyInfo}}}";
        }
    }
}
