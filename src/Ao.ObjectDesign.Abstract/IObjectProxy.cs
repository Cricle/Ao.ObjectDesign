using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public interface IObjectProxy
    {
        object Instance { get; }

        Type Type { get; }

        IEnumerable<IPropertyProxy> GetPropertyProxies();
    }
}
