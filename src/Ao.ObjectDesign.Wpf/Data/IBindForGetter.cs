using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IBindForGetter
    {
        BindForAttribute Get(PropertyInfo info);

        BindForAttribute Get(Type info);
    }
}
