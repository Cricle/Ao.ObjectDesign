using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Wpf;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session
{
    public class DesignStatePropertyContextsDecorater<TScene,TDesignObject> : IPropertyContextsDecorater<WpfTemplateForViewBuildContext,TScene,TDesignObject>
        where TScene:IDesignScene<TDesignObject>
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
