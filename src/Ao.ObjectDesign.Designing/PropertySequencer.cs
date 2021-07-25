using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public class PropertySequencer : Sequencer<IModifyDetail>
    {
        private readonly HashSet<IgnoreIdentity> ignores = new HashSet<IgnoreIdentity>();
        protected override void BeginUndo(IModifyDetail fallback)
        {
            IgnoreIdentity identity = new IgnoreIdentity(fallback.Instance, fallback.PropertyName);
            ignores.Add(identity);
            base.BeginUndo(fallback);
        }
        protected override void EndUndo(IModifyDetail fallback, bool succeed)
        {
            IgnoreIdentity identity = new IgnoreIdentity(fallback.Instance, fallback.PropertyName);
            ignores.Remove(identity);
            base.EndUndo(fallback, succeed);
        }
        protected override bool IsSequenceIgnore(object sender, PropertyChangeToEventArgs e)
        {
            IgnoreIdentity identity = new IgnoreIdentity(sender, e.PropertyName);
            return ignores.Contains(identity);
        }
        protected override IModifyDetail CreateFallback(object sender, PropertyChangeToEventArgs e)
        {
            return new ModifyDetail(sender, e.PropertyName, e.From, e.To);
        }

        protected override IModifyDetail Reverse(IModifyDetail fallback)
        {
            return fallback.Reverse();
        }
    }
}
