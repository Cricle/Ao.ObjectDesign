using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.WpfDesign
{
    public class EmitInstanceFactory : InstanceFactory<EmitInstanceFactory>
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
