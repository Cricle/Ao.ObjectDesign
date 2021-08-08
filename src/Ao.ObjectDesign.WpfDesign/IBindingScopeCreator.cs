using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IBindingScopeCreator
    {
        IEnumerable<IBindingScope> CreateBindingScopes();
    }
}
