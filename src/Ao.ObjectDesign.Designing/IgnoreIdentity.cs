using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public readonly struct IgnoreIdentity : IEquatable<IgnoreIdentity>
    {
        public readonly object Instance;

        public readonly string PropertyName;

        public IgnoreIdentity(object instance, string propertyName)
        {
            Instance = instance;
            PropertyName = propertyName;
        }

        public bool Equals(IgnoreIdentity other)
        {
            return Instance == other.Instance &&
                PropertyName == other.PropertyName;
        }
        public override bool Equals(object obj)
        {
            if (obj is IgnoreIdentity identity)
            {
                return Equals(identity);
            }
            return false;
        }
        public override string ToString()
        {
            return $"{{{Instance}, {PropertyName}}}";
        }

        public override int GetHashCode()
        {
            int h = 0;
            if (Instance != null)
            {
                h = Instance.GetHashCode();
            }
            if (!string.IsNullOrEmpty(PropertyName))
            {
                if (h == 0)
                {
                    h = PropertyName.GetHashCode();
                }
                else
                {
                    h ^= PropertyName.GetHashCode();
                }
            }
            return h;
        }
    }
}
