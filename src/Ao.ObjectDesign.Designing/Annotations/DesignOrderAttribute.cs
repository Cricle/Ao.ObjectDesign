using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DesignOrderAttribute : Attribute
    {
        public DesignOrderAttribute()
        {
        }

        public DesignOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; set; }
    }
}
