using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWithSourceBindingScopeCreator
    {
        IEnumerable<IWithSourceBindingScope> CreateWithSourceBindingScopes();
    }
}
