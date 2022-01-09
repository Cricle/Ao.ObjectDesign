using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public class WpfLazyBindingBox<TDesignObject> : LazyBindingBox<UIElement, TDesignObject, IWithSourceBindingScope, BindingExpressionBase>
    {
        public WpfLazyBindingBox(IDesignPair<UIElement, TDesignObject> pair, IReadOnlyList<IWithSourceBindingScope> bindingScopes) : base(pair, bindingScopes)
        {
        }

        protected override BindingExpressionBase Bind(IWithSourceBindingScope scope, IDesignPair<UIElement, TDesignObject> pair)
        {
            return scope.Bind(Pair.UI);
        }
    }
}
