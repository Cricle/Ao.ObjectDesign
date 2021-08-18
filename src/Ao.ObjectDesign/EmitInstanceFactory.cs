using System;
using System.Diagnostics;

namespace Ao.ObjectDesign
{
    public class EmitInstanceFactory : InstanceFactory
    {
        public EmitInstanceFactory(Type targetType)
            :base(targetType)
        {
        }

        public override object Create()
        {
            Debug.Assert(TargetType != null);

            return ReflectionHelper.Create(TargetType);
        }

    }
}
