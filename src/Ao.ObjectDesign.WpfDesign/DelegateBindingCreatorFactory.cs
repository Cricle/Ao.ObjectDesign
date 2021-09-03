using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public class DelegateBindingCreatorFactory<TDesignObject> : IBindingCreatorFactory<TDesignObject>
    {
        public DelegateBindingCreatorFactory(Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<TDesignObject>>> createFunc)
            :this(createFunc,null)
        {

        }
        public DelegateBindingCreatorFactory(Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<TDesignObject>>> createFunc, 
            Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, bool> isAcceptFunc)
        {
            CreateFunc = createFunc;
            IsAcceptFunc = isAcceptFunc;
        }

        public int Order { get; set; }

        public Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<TDesignObject>>> CreateFunc { get; } 

        public Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, bool> IsAcceptFunc { get; }

        public IEnumerable<IBindingCreator<TDesignObject>> Create(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            Debug.Assert(CreateFunc != null);

            return CreateFunc(unit, state);
        }

        public bool IsAccept(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            if (IsAcceptFunc is null)
            {
                return true;
            }
            return IsAcceptFunc(unit, state);
        }
    }
}
