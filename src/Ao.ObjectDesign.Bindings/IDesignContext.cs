using Ao.ObjectDesign.Designing;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignContext<TUI,TContext>: IDesignBoundsContext
    {
        IDesignSuface<TUI,TContext> DesignSuface { get; }

        IReadOnlyList<IDesignMetedata<TUI,TContext>> DesignMetedatas { get; }

        IActionSequencer<IModifyDetail> Sequencer { get; }
    }
}
