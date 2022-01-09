using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Bindings
{
    public class DelegateBindingCreatorFactory<TUI, TDesignObject, TBindingScope> : IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>
    {
        public DelegateBindingCreatorFactory(Func<IDesignPair<TUI, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>>> createFunc)
            : this(createFunc, null)
        {

        }
        public DelegateBindingCreatorFactory(Func<IDesignPair<TUI, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>>> createFunc,
            Func<IDesignPair<TUI, TDesignObject>, IBindingCreatorState, bool> isAcceptFunc)
        {
            CreateFunc = createFunc;
            IsAcceptFunc = isAcceptFunc;
        }

        public int Order { get; set; }

        public Func<IDesignPair<TUI, TDesignObject>, IBindingCreatorState, IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>>> CreateFunc { get; }

        public Func<IDesignPair<TUI, TDesignObject>, IBindingCreatorState, bool> IsAcceptFunc { get; }

        public IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> Create(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            Debug.Assert(CreateFunc != null);

            return CreateFunc(unit, state);
        }

        public bool IsAccept(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            if (IsAcceptFunc is null)
            {
                return true;
            }
            return IsAcceptFunc(unit, state);
        }
    }
}
