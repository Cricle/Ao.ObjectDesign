using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.Environment
{
    public interface IEngineEnvironment<TScene,TSetting>
        where TScene:IDesignScene<TSetting>
    {
        IFileSystem FileSystem { get; }

        IBindingCreatorStateCreator<TSetting> BindingCreatorStateCreator { get; }

        WpfDesignClipboardManager<TSetting> CreateClipboardManager(SceneEngine<TScene, TSetting> engine);

        SessionManager<TScene,TSetting> CreateSessionManager(SceneEngine<TScene,TSetting> engine);
    }
}
