using Ao.ObjectDesign.Designing.Level;
using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public interface IDesignSessionSettings<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        IFileInfo TargetFile { get; }

        IDirectoryInfo WorkSpace { get; }

        SceneEngine<TScene, TSetting> Engine { get; }

        TScene Scene { get; }

        void Check();
    }
}
