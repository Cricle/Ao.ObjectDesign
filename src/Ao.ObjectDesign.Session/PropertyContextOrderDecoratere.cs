using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Session
{
    public class PropertyContextOrderDecoratere<TScene,TDesignObject> : IPropertyContextsDecorater<WpfTemplateForViewBuildContext,TScene,TDesignObject>
        where TScene :IDesignScene<TDesignObject>
    {
        public PropertyContextOrderDecoratere(DesignOrderManager orderManager)
        {
            OrderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));
        }

        public DesignOrderManager OrderManager { get; }

        public IEnumerable<WpfTemplateForViewBuildContext> Decorate(IPropertyPanel<TScene, TDesignObject> panel, IEnumerable<WpfTemplateForViewBuildContext> contexts)
        {
            return Sort(contexts);
        }
        private IEnumerable<WpfTemplateForViewBuildContext> Sort(IEnumerable<WpfTemplateForViewBuildContext> contexts)
        {
            return contexts.OrderBy(x =>
            {
                var info = x.PropertyProxy.PropertyInfo;
                var identity = new PropertyIdentity(info.DeclaringType, info.Name);
                if (!OrderManager.TryGetValue(identity, out var idx))
                {
                    idx = int.MaxValue;
                }
                return idx;
            });
        }
    }
}
