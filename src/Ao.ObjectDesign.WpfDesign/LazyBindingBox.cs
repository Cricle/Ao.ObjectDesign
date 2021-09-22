using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public class LazyBindingBox<TDesignObject>
    {
        public LazyBindingBox(IDesignPair<UIElement, TDesignObject> pair, IReadOnlyList<IWithSourceBindingScope> bindingScopes)
        {
            Pair = pair;
            BindingScopes = bindingScopes;
        }

        public IDesignPair<UIElement, TDesignObject> Pair { get; }

        public IReadOnlyList<IWithSourceBindingScope> BindingScopes { get; }

        public void ExecuteBindingNoReturn()
        {
            var c = BindingScopes.Count;
            for (int i = 0; i < c; i++)
            {
                BindingScopes[c].Bind(Pair.UI);
            }
        }
        public IReadOnlyList<BindingExpressionBase> ExecuteBinding()
        {
            var c = BindingScopes.Count;
            var res = new BindingExpressionBase[c];
            for (int i = 0; i < c; i++)
            {
                res[i] = BindingScopes[c].Bind(Pair.UI);
            }
            return res;
        }
    }
}
