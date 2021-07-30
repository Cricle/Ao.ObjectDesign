using System;
using System.Reflection;

namespace Ao.ObjectDesign.Designing
{
    public class ModifyDetail : IModifyDetail
    {
        public ModifyDetail(object instance, string propertyName, object from, object to)
        {
            Instance = instance;
            PropertyName = propertyName;
            From = from;
            To = to;
        }

        public object Instance { get; }

        public string PropertyName { get; }

        public object From { get; }

        public object To { get; }

        public FallbackMode Mode { get; protected set; }

        public virtual IModifyDetail Copy(FallbackMode? mode)
        {
            return new ModifyDetail(Instance, PropertyName, From, To) { Mode = mode ?? Mode };
        }

        public virtual void Fallback()
        {
            PropertyInfo prop = Instance.GetType().GetProperty(PropertyName);
            prop.SetValue(Instance, From);
        }

        public virtual IgnoreIdentity? GetIgnoreIdentity()
        {
            return new IgnoreIdentity(Instance, PropertyName);
        }

        public virtual IModifyDetail Reverse()
        {
            return new ModifyDetail(Instance, PropertyName, To, From) 
            {
                Mode= FallbackMode.Reverse
            };
        }

        IFallbackable IFallbackable.Copy(FallbackMode? mode)
        {
            return Copy(mode);
        }

        IFallbackable IFallbackable.Reverse()
        {
            return Reverse();
        }

    }
}
