using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingCreator<TUI,TDesignObject,TBindingScope>
    {
        IDesignPair<TUI, TDesignObject> DesignUnit { get; }

        IBindingCreatorState State { get; }

        IEnumerable<TBindingScope> BindingScopes { get; }

        void Attack();

        void Detack();
    }
}
