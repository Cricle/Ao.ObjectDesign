using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
