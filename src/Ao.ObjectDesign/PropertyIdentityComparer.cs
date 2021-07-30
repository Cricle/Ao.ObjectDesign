using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign
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
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return obj.GetHashCode();
        }
    }
}
