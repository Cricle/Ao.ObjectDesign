using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PlatformTargetMethodAttribute : Attribute
    {
        public PlatformTargetMethodAttribute()
        {
            AutoAnalysis = true;
        }
        public PlatformTargetMethodAttribute(string property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
        }

        public bool AutoAnalysis { get; }

        public string Property { get; }

    }
}
