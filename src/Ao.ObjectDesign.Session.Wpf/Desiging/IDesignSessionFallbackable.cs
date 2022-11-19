using Ao.ObjectDesign.Designing;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public interface IDesignSessionFallbackable
    {
        IActionSequencer<IFallbackable> Sequencer { get; }
    }
}
