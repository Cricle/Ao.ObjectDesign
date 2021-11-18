using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public class CompiledPropertySequencer : PropertySequencer
    {
        protected override IFallbackable CreateFallback(object sender, PropertyChangeToEventArgs e)
        {
            return new CompiledModifyDetail(sender, e.PropertyName, e.From, e.To);
        }
    }
}
