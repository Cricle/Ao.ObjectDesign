using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Reflection;

namespace Ao.ObjectDesign.Designing.Data
{
    public interface IBindForGetter
    {
        BindForAttribute Get(PropertyInfo info);

        BindForAttribute Get(Type type);
    }
}
