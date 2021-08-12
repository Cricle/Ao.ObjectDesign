using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.WpfDesign
{
    public class RefelectionInstanceFactory : InstanceFactory
    {
        public RefelectionInstanceFactory(Type targetType)
            : base(targetType)
        {
        }

        public override object Create()
        {
            Debug.Assert(TargetType != null);

            return Activator.CreateInstance(TargetType);
        }

    }
}
