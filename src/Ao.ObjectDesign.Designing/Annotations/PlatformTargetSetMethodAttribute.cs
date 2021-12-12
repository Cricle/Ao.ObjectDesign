using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PlatformTargetSetMethodAttribute : PlatformTargetMethodAttribute
    {
        public PlatformTargetSetMethodAttribute()
        {
        }

        public PlatformTargetSetMethodAttribute(string property) : base(property)
        {
        }
    }
}
