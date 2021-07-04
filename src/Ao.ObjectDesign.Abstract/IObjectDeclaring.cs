using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public interface IObjectDeclaring
    {
        Type Type { get; }

        IEnumerable<IPropertyDeclare> GetPropertyDeclares();
    }
}