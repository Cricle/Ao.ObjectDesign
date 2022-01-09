using System.Collections.Generic;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingScopeCreator<TBinding, TExpression, TObject>
    {
        IEnumerable<IBindingScope<TBinding, TExpression, TObject>> CreateBindingScopes();
    }
}
