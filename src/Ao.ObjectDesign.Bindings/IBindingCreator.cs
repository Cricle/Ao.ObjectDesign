using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingCreator<TUI, TDesignObject, TBindingScope>
    {
        IDesignPair<TUI, TDesignObject> DesignUnit { get; }

        IBindingCreatorState State { get; }

        IEnumerable<TBindingScope> BindingScopes { get; }

        void Attack();

        void Detack();
    }
}
