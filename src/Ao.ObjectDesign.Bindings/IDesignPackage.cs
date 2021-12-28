using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignPackage<TUI,TDesignObject,TBindingScope>
    {
        UIDesignMap UIDesinMap { get; }

        IEnumerable<IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>> GetBindingCreatorFactorys(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state);

        IBindingCreatorState CreateBindingCreatorState(IDesignPair<TUI, TDesignObject> unit);
    }
}
