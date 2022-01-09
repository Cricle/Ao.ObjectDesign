using Ao.ObjectDesign.Designing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Bindings
{
    public abstract partial class DesignContext<TUI, TContext> : DesignInitContext, IDesignContext<TUI, TContext>
    {
        protected DesignContext(IServiceProvider provider,
            IEnumerable<TUI> target,
            IActionSequencer<IModifyDetail> sequencer,
            IDesignSuface<TUI, TContext> designSuface)
            : base(provider)
        {
            DesignSuface = designSuface;
            DesignMetedatas = target.Select(x => CreateDesignMetedata(x)).ToList();
            Sequencer = sequencer;
        }
        protected DesignContext(IServiceProvider provider,
            IDesignMetedata<TUI, TContext>[] metedatas,
            IActionSequencer<IModifyDetail> sequencer,
            IDesignSuface<TUI, TContext> designSuface)
            : base(provider)
        {
            DesignMetedatas = metedatas;
            Sequencer = sequencer;
            DesignSuface = designSuface;
        }
        public IDesignSuface<TUI, TContext> DesignSuface { get; }

        public IReadOnlyList<IDesignMetedata<TUI, TContext>> DesignMetedatas { get; }

        public IActionSequencer<IModifyDetail> Sequencer { get; }

        protected abstract IDesignMetedata<TUI, TContext> CreateDesignMetedata(TUI element);
    }
}
