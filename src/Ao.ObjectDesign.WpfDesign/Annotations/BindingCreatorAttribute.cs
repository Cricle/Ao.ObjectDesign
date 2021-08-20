using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class BindingCreatorAttribute : Attribute
    {
        public BindingCreatorAttribute(Type creatorType)
        {
            CreatorType = creatorType ?? throw new ArgumentNullException(nameof(creatorType));
        }

        public Type CreatorType { get; }
    }
}
