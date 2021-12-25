using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.EngineConfig
{
    public static class EngineConfigurationCheckExtensions
    {
        public static void EnsureNotNull<TScene, TSetting>(this IEngineConfiguration<TScene, TSetting> configuration)
            where TScene : IDesignScene<TSetting>
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (configuration.EngineEnvironment is null)
            {
                throw new ArgumentNullException(nameof(IEngineConfiguration<TScene, TSetting>.EngineEnvironment));
            }
        }
    }
}
