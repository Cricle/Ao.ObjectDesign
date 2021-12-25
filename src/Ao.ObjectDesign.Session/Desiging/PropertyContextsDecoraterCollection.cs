using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class PropertyContextsDecoraterCollection<TContext,TScene,TSetting> : List<IPropertyContextsDecorater<TContext, TScene, TSetting>>, IPropertyContextsDecorater<TContext, TScene, TSetting>
        where TScene:IDesignScene<TSetting>
    {
        public PropertyContextsDecoraterCollection()
        {
        }

        public PropertyContextsDecoraterCollection(int capacity) : base(capacity)
        {
        }

        public PropertyContextsDecoraterCollection(IEnumerable<IPropertyContextsDecorater<TContext, TScene, TSetting>> collection)
            : base(collection)
        {
        }

        public IEnumerable<TContext> Decorate(IPropertyPanel<TScene,TSetting> panel, IEnumerable<TContext> contexts)
        {
            var ctxs = contexts;
            for (int i = 0; i < Count; i++)
            {
                ctxs = this[i].Decorate(panel, ctxs);
            }
            return ctxs;
        }
    }
}
