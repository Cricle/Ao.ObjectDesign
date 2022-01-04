using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;

namespace Ao.ObjectDesign.Session.BuildIn
{
    public partial class SceneMakerStartup<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        public abstract IDesignSessionSettings<TScene, TSetting> CreateSessionSettings(TScene scene);
    }
}
