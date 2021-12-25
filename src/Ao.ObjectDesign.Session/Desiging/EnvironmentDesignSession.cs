using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Session.Desiging
{
    [DebuggerDisplay("{Id}")]
    public class EnvironmentDesignSession<TScene,TSetting> : DesignSession<TScene,TSetting>
        where TScene:IObservableDesignScene<TSetting>
    {
        public EnvironmentDesignSession(IDesignSessionSettings<TScene, TSetting> settings,
            WpfObjectDesignerSettings designerSettings,
            TemplateContextsDecoraterCollection<TScene, TSetting> decoraters)
            : base(settings)
        {
            Decoraters = decoraters ?? throw new ArgumentNullException(nameof(decoraters));
            DesignerSettings = designerSettings ?? throw new ArgumentNullException(nameof(designerSettings));
        }

        public WpfObjectDesignerSettings DesignerSettings { get; }

        public TemplateContextsDecoraterCollection<TScene, TSetting> Decoraters { get; }

        protected override WpfSceneManager<TScene, TSetting> CreateSceneManager(WpfObjectDesigner designer)
        {
            return new AutoSceneManager<TScene,TSetting>(Engine.DesignPackage, ElementContriner) { LazyBinding = LazyBinding, CurrentScene = Scene };
        }

        protected override WpfObjectDesigner CreateObjectDesigner()
        {
            return new WpfObjectDesigner(DesignerSettings);
        }

        protected override IPropertyPanel<TScene, TSetting> CreatePropertyPanel()
        {
            return new DataTemplatePropertyPanel<TScene,TSetting>(this, Decoraters);
        }
    }
}
