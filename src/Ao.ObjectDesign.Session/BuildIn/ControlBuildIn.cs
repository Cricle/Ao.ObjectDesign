using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System;

namespace Ao.ObjectDesign.Session.BuildIn
{
    public partial class ControlBuildIn<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        private ControlBuildIn() { }

        private SceneEngine<TScene, TSetting> engine;

        public SceneEngine<TScene, TSetting> Engine => engine;

        public static ControlBuildIn<TScene, TSetting> FromEngine(SceneEngine<TScene, TSetting> engine)
        {
            if (engine is null)
            {
                throw new ArgumentNullException(nameof(engine));
            }
            engine.ThrowIfNoInitialized();
            return new ControlBuildIn<TScene, TSetting> { engine = engine };
        }
    }
}
