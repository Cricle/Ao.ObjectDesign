using Ao.ObjectDesign.Designing;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IDesignSessionFallbackable
    {
        IActionSequencer<IFallbackable> Sequencer { get; }
    }
}
