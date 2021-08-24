using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class UnfoldMappingAttribute : Attribute
    {
        public bool SkipSelft { get; set; } = true;
    }
}
