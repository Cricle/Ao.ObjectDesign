using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.EngineConfig
{
    public interface IEngineConfiguration<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        IEngineEnvironment<TScene, TSetting> EngineEnvironment { get; }

        IServiceProvider ServiceProvider { get; }
    }
}
