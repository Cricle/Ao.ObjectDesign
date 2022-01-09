using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>
    {
        int Order { get; }

        bool IsAccept(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state);

        IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> Create(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state);
    }
}
