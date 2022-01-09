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

        public FallbackModes Mode { get; protected set; }

        public virtual IModifyDetail Copy(FallbackModes? mode)
        {
            return new ModifyDetail(Instance, PropertyName, From, To) { Mode = mode ?? Mode };
        }

        public virtual void Fallback()
        {
            PropertyInfo prop = Instance.GetType().GetProperty(PropertyName);
            if (prop is null)
            {
                throw new InvalidOperationException($"Instance {Instance} has not property {PropertyName}");
            }
            prop.SetValue(Instance, From);
        }

        public virtual IgnoreIdentity? GetIgnoreIdentity()
        {
            return new IgnoreIdentity(Instance, PropertyName);
        }
        public bool IsReverse(IModifyDetail fallbackable)
        {
            return IsReverse((IFallbackable)fallbackable);
        }
        public bool IsReverse(IFallbackable fallbackable)
        {
            if (fallbackable is IModifyDetail detail)
            {
                if (detail.Instance != Instance ||
                    detail.PropertyName != PropertyName)
                {
                    return false;
                }
                if (detail.From is null && To != null ||
                    detail.To is null && From != null)
                {
                    return false;
                }
                if (detail.From is null && To is null &&
                    detail.To is null && From is null)
                {
                    return true;
                }
                var a = detail.From?.Equals(To);
                var b = detail.To?.Equals(From);
                if (a is null || b is null)
                {
                    return true;
                }
                return a is null || a.Value &&
                    b is null || b.Value;
            }
            return false;
        }

        public virtual IModifyDetail Reverse()
        {
            return new ModifyDetail(Instance, PropertyName, To, From)
            {
                Mode = FallbackModes.Reverse
            };
        }

        IFallbackable IFallbackable.Copy(FallbackModes? mode)
        {
            return Copy(mode);
        }

        IFallbackable IFallbackable.Reverse()
        {
            return Reverse();
        }

    }
}
