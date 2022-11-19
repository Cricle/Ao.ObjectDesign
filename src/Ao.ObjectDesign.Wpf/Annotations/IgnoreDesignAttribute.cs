using System;

namespace Ao.ObjectDesign.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class IgnoreDesignAttribute : Attribute
    {
    }
}
