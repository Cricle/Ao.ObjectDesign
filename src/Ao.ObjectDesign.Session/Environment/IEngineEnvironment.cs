using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Environment
{
    public interface IEngineEnvironment<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        IFileSystem FileSystem { get; }

        IBindingCreatorStateCreator<TSetting> BindingCreatorStateCreator { get; }

        DesignClipboardManager<TSetting> CreateClipboardManager(SceneEngine<TScene, TSetting> engine);

        SessionManager<TScene, TSetting> CreateSessionManager(SceneEngine<TScene, TSetting> engine);

        IServiceProvider ServiceProvider { get; }
    }
}
