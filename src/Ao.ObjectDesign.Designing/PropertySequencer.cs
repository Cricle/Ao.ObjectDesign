using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public class PropertySequencer : Sequencer<IFallbackable>
    {
        protected override IFallbackable CreateFallback(object sender, PropertyChangeToEventArgs e)
        {
            return new ModifyDetail(sender, e.PropertyName, e.From, e.To);
        }
    }
}
