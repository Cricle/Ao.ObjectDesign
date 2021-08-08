using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class InstanceFactory<T> : IInstanceFactory, IEquatable<T>
        where T:IInstanceFactory
    {
        protected InstanceFactory(Type targetType)
        {
            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public virtual Type TargetType { get; }

        public abstract object Create();

        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                return Equals((T)obj);
            }
            return false;
        }
        public override int GetHashCode()
        {
            Debug.Assert(TargetType != null);
            return TargetType.GetHashCode();
        }

        public bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TargetType == TargetType;
        }
    }
}
