using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    internal class PropertyIdentityComparer : IEqualityComparer<PropertyIdentity>
    {
        public static readonly PropertyIdentityComparer Instance = new PropertyIdentityComparer();

        public bool Equals(PropertyIdentity x, PropertyIdentity y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(PropertyIdentity obj)
        {
            return obj.GetHashCode();
        }
    }
}
