using System;

namespace Ao.ObjectDesign.Session.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class HasInstanceCreatorAttribute : Attribute
    {
        public HasInstanceCreatorAttribute()
        {
        }

        public HasInstanceCreatorAttribute(Type creatorType)
        {
            CreatorType = creatorType;
        }

        public Type CreatorType { get; set; }
    }
}
