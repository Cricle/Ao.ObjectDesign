using System;
using System.Diagnostics;

namespace Ao.ObjectDesign
{
    public class EmitInstanceFactory : InstanceFactory
    {
        public EmitInstanceFactory(Type targetType)
            : base(targetType)
        {
            Debug.Assert(targetType != null);
            TypeCreator = CompiledPropertyInfo.GetCreator(targetType);
        }

        public TypeCreator TypeCreator { get; }

        public override object Create()
        {
            Debug.Assert(TargetType != null);
            Debug.Assert(TypeCreator != null);

            return TypeCreator();
        }

    }
}
