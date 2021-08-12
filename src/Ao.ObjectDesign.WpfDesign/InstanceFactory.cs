using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class InstanceFactory : IInstanceFactory
    {
        protected InstanceFactory(Type targetType)
        {
            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public virtual Type TargetType { get; }

        public abstract object Create();

        public override bool Equals(object obj)
        {
            if (obj is InstanceFactory factory)
            {
                return factory.TargetType == TargetType;
            }
            return false;
        }
        public override int GetHashCode()
        {
            Debug.Assert(TargetType != null);
            return TargetType.GetHashCode();
        }
    }
}
