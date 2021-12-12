using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PlatformTargetGetMethodAttribute : PlatformTargetMethodAttribute
    {
        public PlatformTargetGetMethodAttribute()
        {
        }

        public PlatformTargetGetMethodAttribute(string property) : base(property)
        {
        }
    }
}
