using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class BindingCreator<TUI, TDesignObject, TBindingScope> : IBindingCreator<TUI, TDesignObject, TBindingScope>
    {
        protected BindingCreator(IDesignPair<TUI, TDesignObject> designUnit, IBindingCreatorState state)
        {
            DesignUnit = designUnit;
            State = state;
        }

        public IDesignPair<TUI, TDesignObject> DesignUnit { get; }

        public IBindingCreatorState State { get; }

        public virtual IEnumerable<TBindingScope> BindingScopes => CreateBindingScopes();

        public virtual void Attack()
        {

        }

        public virtual void Detack()
        {
        }

        protected virtual IEnumerable<TBindingScope> CreateBindingScopes()
        {
            yield break;
        }
    }
}
