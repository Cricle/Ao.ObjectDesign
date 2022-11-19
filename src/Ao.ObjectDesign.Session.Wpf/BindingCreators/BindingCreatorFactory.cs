using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using Ao.ObjectDesign.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf.BindingCreators
{
    public abstract class BindingCreatorFactory<TDesignObject> : IBindingCreatorFactory<UIElement, TDesignObject, IWithSourceBindingScope>
    {
        public virtual int Order { get; set; }

        public IEnumerable<IBindingCreator<UIElement, TDesignObject, IWithSourceBindingScope>> Create(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            foreach (var item in CreateWpfCreators(unit, state))
            {
                yield return item;
            }
        }
        protected abstract IEnumerable<IWpfBindingCreator<TDesignObject>> CreateWpfCreators(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state);

        public abstract bool IsAccept(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state);
    }

}
