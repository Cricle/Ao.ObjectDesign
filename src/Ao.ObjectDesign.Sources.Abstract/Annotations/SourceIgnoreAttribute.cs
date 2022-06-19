using System;

namespace Ao.ObjectDesign.Sources.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class SourceIgnoreAttribute : Attribute
    {
    }
}
