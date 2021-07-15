using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    public interface IBindingToTypeCreator
    {
        IEnumerable<BindingUnit> CreateBindings(Type type);

        bool IsSupportCreateBind(Type type);
    }

}
