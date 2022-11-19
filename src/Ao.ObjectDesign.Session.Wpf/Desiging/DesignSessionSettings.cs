using Ao.ObjectDesign.Designing.Level;
using System;
using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public class DesignSessionSettings<TScene, TSetting> : IDesignSessionSettings<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        public IFileInfo TargetFile { get; set; }

        public IDirectoryInfo WorkSpace { get; set; }

        public SceneEngine<TScene, TSetting> Engine { get; set; }

        public TScene Scene { get; set; }

        public void Check()
        {
            ThrowIfNull(TargetFile, nameof(TargetFile));
            ThrowIfNull(WorkSpace, nameof(WorkSpace));
            ThrowIfNull(Engine, nameof(Engine));
            ThrowIfNull(Scene, nameof(Scene));
        }
        private static void ThrowIfNull<T>(T instance, string name)
        {
            if (instance == null)
            {
                throw new ArgumentNullException($"Property {name} is null!");
            }
        }
    }
}
