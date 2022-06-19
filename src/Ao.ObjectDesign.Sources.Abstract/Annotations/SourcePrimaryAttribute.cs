using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.ObjectDesign.Sources.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class SourcePrimaryAttribute : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class SourceNameAttribute : Attribute
    {
        public SourceNameAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}
