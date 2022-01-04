using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Session.EngineConfig;
using Ao.ObjectDesign.Session.Environment;
using System;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Session.BuildIn
{
    public abstract partial class SceneMakerStartup<TScene, TSetting> : InitableObject, ISceneMakerStartup<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        private SceneEngine<TScene, TSetting> engine;
        private IEngineConfiguration<TScene, TSetting> configuration;

        public SceneEngine<TScene, TSetting> Engine => engine;

        public IEngineConfiguration<TScene, TSetting> Configuration => configuration;

        protected override void OnInitialize()
        {
            configuration = GetConfiguration();
            engine = CreateEngine(configuration);
            engine.Initialize();
        }

        public virtual void Ready()
        {

        }

        protected abstract IEngineEnvironment<TScene, TSetting> GetEnvironment();

        protected abstract IServiceProvider GetServiceProvider();

        protected virtual IEngineConfiguration<TScene, TSetting> GetConfiguration()
        {
            var env = GetEnvironment();
            var provider = GetServiceProvider();
            return new EngineConfiguration<TScene, TSetting>
            {
                EngineEnvironment = env,
                ServiceProvider = provider
            };
        }

        protected virtual SceneEngine<TScene, TSetting> CreateEngine(IEngineConfiguration<TScene, TSetting> configuration)
        {
            return new SceneEngine<TScene, TSetting>(configuration);
        }


        public ControlBuildIn<TScene, TSetting> CreateBuildIn()
        {
            CheckState();

            return ControlBuildIn<TScene, TSetting>.FromEngine(engine);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void CheckState()
        {
            this.ThrowIfNoInitialized();
            ThrowIfDisposed();
        }

        public virtual IDesignSession<TScene, TSetting> NewSession(TScene scene)
        {
            this.ThrowIfNoInitialized();

            var sessionSettings = CreateSessionSettings(scene);
            var session = Engine.SessionManager.Create(sessionSettings);
            CreatingSession(session);
            Engine.SessionManager.InitializeSession(session);
            return session;
        }
        protected virtual void CreatingSession(IDesignSession<TScene, TSetting> session)
        {

        }
        public bool RemoveSession(Guid id)
        {
            this.ThrowIfNoInitialized();
            if (engine.SessionManager.SessionMap.TryGetValue(id, out var session))
            {
                OnRemovingSession(session);
            }
            return Engine.SessionManager.Remove(id);
        }
        protected virtual void OnRemovingSession(IDesignSession<TScene, TSetting> session)
        {

        }

    }
}
