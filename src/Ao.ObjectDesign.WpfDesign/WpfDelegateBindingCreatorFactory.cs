using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public class WpfDelegateBindingCreatorFactory<TDesignObject> : DelegateBindingCreatorFactory<UIElement, TDesignObject, IWithSourceBindingScope>
    {
        public WpfDelegateBindingCreatorFactory(Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<UIElement, TDesignObject, IWithSourceBindingScope>>> createFunc) : base(createFunc)
        {
        }

        public WpfDelegateBindingCreatorFactory(Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<UIElement, TDesignObject, IWithSourceBindingScope>>> createFunc, Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, bool> isAcceptFunc) : base(createFunc, isAcceptFunc)
        {
        }
    }
}
