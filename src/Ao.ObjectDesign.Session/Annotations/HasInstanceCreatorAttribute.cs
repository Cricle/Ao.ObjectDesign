using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.Annotations
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
