using System;

namespace Ao.ObjectDesign.Designing
{
    public class CompiledModifyDetail : ModifyDetail
    {
        public CompiledModifyDetail(object instance, string propertyName, object from, object to)
            : base(instance, propertyName, from, to)
        {
        }

        public override void Fallback()
        {
            Type instanceType = Instance.GetType();
            PropertyIdentity identity = new PropertyIdentity(instanceType, PropertyName);
            PropertySetter setter = CompiledPropertyInfo.GetSetter(identity);
            setter(Instance, From);
        }
        public override IModifyDetail Copy(FallbackModes? mode)
        {
            return new CompiledModifyDetail(Instance, PropertyName, From, To) { Mode = mode ?? Mode };
        }
        public override IModifyDetail Reverse()
        {
            return new CompiledModifyDetail(Instance, PropertyName, To, From) { Mode = FallbackModes.Reverse };
        }
    }
}
