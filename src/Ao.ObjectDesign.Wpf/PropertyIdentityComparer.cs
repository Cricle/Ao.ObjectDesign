using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    internal class PropertyIdentityComparer : IEqualityComparer<PropertyIdentity>
    {
        public static readonly PropertyIdentityComparer Instance = new PropertyIdentityComparer();

        public bool Equals(PropertyIdentity x, PropertyIdentity y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            if (x is null || y is null)
            {
                return false;
            }
            return x.Equals(y);
        }

        public int GetHashCode(PropertyIdentity obj)
        {
            return obj.GetHashCode();
        }
    }
}
