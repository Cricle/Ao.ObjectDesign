using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using System;
using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Environment
{
    public abstract class EngineEnvironmentBase<TScene, TDesignObject> : IEngineEnvironment<TScene, TDesignObject>
        where TScene : IObservableDesignScene<TDesignObject>
    {
        protected EngineEnvironmentBase(IFileSystem fileSystem,
            ISceneFetcher<TScene> sceneFetcher,
            SceneEngine<TScene, TDesignObject> engine,
            IServiceProvider serviceProvider)
        {
            FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            SceneFetcher = sceneFetcher ?? throw new ArgumentNullException(nameof(sceneFetcher));
            engine = engine ?? throw new ArgumentNullException(nameof(engine));
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            engine.ThrowIfNoInitialized();
            BindingCreatorStateCreator = new BindingCreatorStateCreator<TScene, TDesignObject>(engine.BindingCreatorStateDecoraters, engine.ServiceProvider);
        }
        protected EngineEnvironmentBase(IFileSystem fileSystem,
           ISceneFetcher<TScene> sceneFetcher,
           IBindingCreatorStateCreator<TDesignObject> creator,
            IServiceProvider serviceProvider)
        {
            FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            SceneFetcher = sceneFetcher ?? throw new ArgumentNullException(nameof(sceneFetcher));
            BindingCreatorStateCreator = creator ?? throw new ArgumentNullException(nameof(creator));
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IFileSystem FileSystem { get; }

        public ISceneFetcher<TScene> SceneFetcher { get; }

        public IBindingCreatorStateCreator<TDesignObject> BindingCreatorStateCreator { get; }

        public IServiceProvider ServiceProvider { get; }

        public abstract DesignClipboardManager<TDesignObject> CreateClipboardManager(SceneEngine<TScene, TDesignObject> engine);

        public SessionManager<TScene, TDesignObject> CreateSessionManager(SceneEngine<TScene, TDesignObject> engine)
        {
            return new EnvironmentSessionManager<TScene, TDesignObject>(engine, engine.TemplateContextsDecoraters);
        }
    }
}
