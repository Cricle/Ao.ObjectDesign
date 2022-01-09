using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
    public sealed class ProvideMulityValuesAttribute : Attribute
    {
    }
}
