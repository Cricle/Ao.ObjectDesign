using Ao.ObjectDesign.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Desiging
{
    public class ActionSequencer : Sequencer<IFallbackable>
    {
        protected override IFallbackable CreateFallback(object sender, PropertyChangeToEventArgs e)
        {
            return new CompiledModifyDetail(sender, e.PropertyName, e.From, e.To);
        }

        protected override IFallbackable Reverse(IFallbackable fallback)
        {
            return fallback.Reverse();
        }
    }
}
