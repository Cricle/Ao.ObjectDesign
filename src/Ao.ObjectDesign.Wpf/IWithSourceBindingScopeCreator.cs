using Ao.ObjectDesign.Data;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWithSourceBindingScopeCreator
    {
        IEnumerable<IWithSourceBindingScope> CreateWithSourceBindingScopes();
    }
}
