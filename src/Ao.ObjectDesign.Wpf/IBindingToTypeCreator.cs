using Ao.ObjectDesign.Data;
using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public interface IBindingToTypeCreator
    {
        IEnumerable<BindingUnit> CreateBindings(Type type);

        bool IsSupportCreateBind(Type type);
    }

}
