﻿using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class TemplateContextsDecoraterCollection<TScene, TSetting> : PropertyContextsDecoraterCollection<WpfTemplateForViewBuildContext, TScene, TSetting>, IPropertyContextsDecorater<WpfTemplateForViewBuildContext, TScene, TSetting>
            where TScene : IDesignScene<TSetting>
    {
        public TemplateContextsDecoraterCollection()
        {
        }

        public TemplateContextsDecoraterCollection(int capacity) : base(capacity)
        {
        }

        public TemplateContextsDecoraterCollection(IEnumerable<IPropertyContextsDecorater<WpfTemplateForViewBuildContext, TScene, TSetting>> collection) : base(collection)
        {
        }
    }
}
