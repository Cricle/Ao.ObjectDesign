using System;

namespace Ao.ObjectDesign.Designing
{
    public interface IDesignSuface<TUI, TContext>
    {
        TUI[] DesigningObjects { get; set; }
        IActionSequencer<IModifyDetail> Sequencer { get; set; }
        IServiceProvider ServiceProvider { get; set; }

        void ClearDesignObjects();
        TContext GetContext();
        void SetDesigningObjects(params TUI[] elements);
        void Update();
    }
}
