using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingCreatorFactory<TUI,TDesignObject,TBindingScope>
    {
        int Order { get; }

        bool IsAccept(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state);

        IEnumerable<IBindingCreator<TUI,TDesignObject,TBindingScope>> Create(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state);
    }
}
