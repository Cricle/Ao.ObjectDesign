using System;
using System.Diagnostics;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public readonly struct TypeProperty : IEquatable<TypeProperty>
    {
        public readonly PropertyInfo PropertyInfo;

        public readonly PropertySetter Setter;

        public readonly PropertyGetter Getter;

        public readonly string Name;

        public readonly bool CanSet;

        public readonly bool CanGet;

        public readonly PropertyIdentity Identity;

        internal TypeProperty(PropertyInfo propertyInfo, PropertySetter setter, PropertyGetter getter, string name, bool canSet, bool canGet, PropertyIdentity identity)
        {
            PropertyInfo = propertyInfo;
            Setter = setter;
            Getter = getter;
            Name = name;
            CanSet = canSet;
            CanGet = canGet;
            Identity = identity;
        }
        public override int GetHashCode()
        {
            Debug.Assert(PropertyInfo != null);
            return PropertyInfo.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is TypeProperty tp)
            {
                return tp.PropertyInfo == PropertyInfo;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{{{PropertyInfo}}}";
        }

        public bool Equals(TypeProperty other)
        {
            return other.PropertyInfo == PropertyInfo;
        }
    }
}
