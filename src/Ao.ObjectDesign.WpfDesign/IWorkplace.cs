using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IWorkplace<TScene, TDesignObject>
        where TScene : IDeisgnScene<TDesignObject>
    {
        IReadOnlyList<string> SceneNames { get; }

        TScene GetScene(string name);

        bool RemoveScene(string name);

        TScene CopyScene(string sourceName, string destName);

        TScene CreateScene(string name);

        void Store(TScene scene);

        void Rename(string oldName, string newName);

        bool HasScene(string name);

        event EventHandler<ActionSceneResultEventArgs> CreatedScene;
        event EventHandler<ActionSceneResultEventArgs> RemovedScene;
        event EventHandler<TwoSceneResultEventArgs> CopiedScene;
        event EventHandler<TwoSceneResultEventArgs> RenamedScene;
        event EventHandler<StoredSceneEventArgs<TScene,TDesignObject>> StoredScene;

    }
}
