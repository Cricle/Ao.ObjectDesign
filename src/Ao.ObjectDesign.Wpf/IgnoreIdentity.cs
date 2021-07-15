using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Wpf
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    internal readonly struct IgnoreIdentity : IEquatable<IgnoreIdentity>
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
            return Instance.GetHashCode() ^ PropertyName.GetHashCode();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
