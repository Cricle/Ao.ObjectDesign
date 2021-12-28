using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public static class DesignContextFindExtensions
    {
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI,TDesignObject,TContext>(this IDesignContext<TUI, TContext> context,
            IDesignSceneController<TUI, TDesignObject> controller,
            Func<TDesignObject, IRect> rectSelector)
        {
            var set = new ReadOnlyHashSet<TUI>(context.DesignMetedatas.Select(x => x.Target));
            return controller.Lookup(set, rectSelector);
        }
    }
}
