using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Environment;
using System;

namespace Ao.ObjectDesign.Session.EngineConfig
{
    public class EngineConfiguration<TScene, TSetting> : IEngineConfiguration<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        public IEngineEnvironment<TScene,TSetting> EngineEnvironment { get; set; }

        public IServiceProvider ServiceProvider { get; set; }
    }
}
