using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.BuildIn
{
    public partial class SceneMakerStartup<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        public abstract IDesignSessionSettings<TScene,TSetting> CreateSessionSettings(TScene scene);
    }
}
