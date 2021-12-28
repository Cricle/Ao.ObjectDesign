using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingScopeCreator<TBinding, TExpression, TObject>
    {
        IEnumerable<IBindingScope<TBinding, TExpression, TObject>> CreateBindingScopes();
    }
}
