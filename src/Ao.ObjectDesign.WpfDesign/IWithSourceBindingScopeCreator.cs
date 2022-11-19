using Ao.ObjectDesign.Data;
using System.Collections.Generic;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWithSourceBindingScopeCreator
    {
        IEnumerable<IWithSourceBindingScope> CreateWithSourceBindingScopes();
    }
}
