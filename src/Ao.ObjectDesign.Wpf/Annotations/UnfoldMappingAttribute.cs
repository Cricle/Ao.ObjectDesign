using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Class)]
    public sealed class UnfoldMappingAttribute : Attribute
    {
    }
}
