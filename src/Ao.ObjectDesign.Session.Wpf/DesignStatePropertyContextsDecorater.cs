using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;

using System.Collections.Generic;

namespace Ao.ObjectDesign.Session.Wpf
{
    public class DesignStatePropertyContextsDecorater<TScene, TDesignObject> : IPropertyContextsDecorater<WpfTemplateForViewBuildContext, TScene, TDesignObject>
        where TScene : IDesignScene<TDesignObject>
    {
        public DesignStatePropertyContextsDecorater(SceneEngine<TScene, TDesignObject> engine)
        {
            Engine = engine;
        }

        public SceneEngine<TScene, TDesignObject> Engine { get; }

        public IEnumerable<WpfTemplateForViewBuildContext> Decorate(IPropertyPanel<TScene, TDesignObject> panel, IEnumerable<WpfTemplateForViewBuildContext> contexts)
        {
            foreach (var item in contexts)
            {
                DesigningDataHelper<TScene, TDesignObject>.SetPropertyPanel(item, panel);
                DesigningDataHelper<TScene, TDesignObject>.SetEngine(item, Engine);
            }
            return contexts;
        }
    }
}
