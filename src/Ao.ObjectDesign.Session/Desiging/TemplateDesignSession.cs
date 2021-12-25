using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class TemplateDesignSession<TScene, TSetting> : DesignSession<TScene, TSetting>
        where TScene : IObservableDesignScene<TSetting>
    {
        public TemplateDesignSession(IDesignSessionSettings<TScene, TSetting> settings) : base(settings)
        {
        }

        protected override WpfObjectDesigner CreateObjectDesigner()
        {
            var setting = Engine.CreateDesignSettings();
            setting.Sequencer = new PropertySequencer();
            return new WpfObjectDesigner(setting);
        }

        protected override IPropertyPanel<TScene, TSetting> CreatePropertyPanel()
        {
            return new DataTemplatePropertyPanel<TScene, TSetting>(this, ContextsDecoraters);
        }
        protected override WpfSceneManager<TScene, TSetting> CreateSceneManager(WpfObjectDesigner designer)
        {
            return new AutoSceneManager<TScene, TSetting>(Engine.DesignPackage, ElementContriner) { LazyBinding = LazyBinding, CurrentScene = Scene };
        }
    }
}
