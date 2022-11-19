using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;
using Ao.ObjectDesign.Session.Wpf.Environment;

namespace Ao.ObjectDesign.Session.Wpf.BuildIn
{
    public interface ISceneMakerStartup<TScene, TSetting> : IInitableObject
        where TScene : IDesignScene<TSetting>
    {
        SceneEngine<TScene, TSetting> Engine { get; }

        IEngineEnvironment<TScene, TSetting> Environment { get; }

        ControlBuildIn<TScene, TSetting> CreateBuildIn();

        IDesignSessionSettings<TScene, TSetting> CreateSessionSettings(TScene scene);

        void Ready();
    }
}
