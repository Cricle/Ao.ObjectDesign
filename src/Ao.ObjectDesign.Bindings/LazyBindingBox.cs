using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class LazyBindingBox<TUI,TDesignObject,TBindingScope,TExpression>
    {
        public LazyBindingBox(IDesignPair<TUI, TDesignObject> pair, IReadOnlyList<TBindingScope> bindingScopes)
        {
            Pair = pair;
            BindingScopes = bindingScopes;
        }

        public IDesignPair<TUI, TDesignObject> Pair { get; }

        public IReadOnlyList<TBindingScope> BindingScopes { get; }

        public void ExecuteBindingNoReturn()
        {
            var c = BindingScopes.Count;
            for (int i = 0; i < c; i++)
            {
                Bind(BindingScopes[i], Pair);
            }
        }
        public IReadOnlyList<TExpression> ExecuteBinding()
        {
            var c = BindingScopes.Count;
            var res = new TExpression[c];
            for (int i = 0; i < c; i++)
            {
                res[i] = Bind(BindingScopes[i], Pair); 
            }
            return res;
        }
        protected abstract TExpression Bind(TBindingScope scope,IDesignPair<TUI,TDesignObject> pair);
    }
}
