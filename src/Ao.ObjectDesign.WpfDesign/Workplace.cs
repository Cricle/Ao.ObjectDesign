using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class Workplace<TScene, TDesignObject> : IWorkplace<TScene, TDesignObject>
        where TScene : IDeisgnScene<TDesignObject>
    {
        public abstract IReadOnlyList<string> SceneNames { get; }

        public event EventHandler<ActionSceneResultEventArgs> CreatedScene;
        public event EventHandler<ActionSceneResultEventArgs> RemovedScene;
        public event EventHandler<TwoSceneResultEventArgs> CopiedScene;
        public event EventHandler<TwoSceneResultEventArgs> RenamedScene;
        public event EventHandler<StoredSceneEventArgs<TScene, TDesignObject>> StoredScene;
        protected void RaiseCreatedScene(ActionSceneResultEventArgs e)
        {
            CreatedScene?.Invoke(this, e);
        }
        protected void RaiseRemovedScene(ActionSceneResultEventArgs e)
        {
            RemovedScene?.Invoke(this, e);
        }
        protected void RaiseCopiedScene(TwoSceneResultEventArgs e)
        {
            CopiedScene?.Invoke(this, e);
        }
        protected void RaiseRenamedScene(TwoSceneResultEventArgs e)
        {
            RenamedScene?.Invoke(this, e);
        }
        protected void RaiseStoredScene(StoredSceneEventArgs<TScene, TDesignObject> e)
        {
            StoredScene?.Invoke(this,e);
        }

        public TScene CopyScene(string sourceName, string destName)
        {
            var copied=CoreCopyScene(sourceName, destName);
            var e = new TwoSceneResultEventArgs(sourceName, destName);
            RaiseCopiedScene(e);
            return copied;
        }
        protected TScene EnsureGetScene(string name)
        {
            var val = GetScene(name);
            if (val == null)
            {
                throw new InvalidOperationException($"Can't find scene {name}");
            }
            return val;
        }
        protected abstract TScene CoreCopyScene(string sourceName, string destName);
        protected abstract void CoreAddScene(TScene scene, string name);
        protected abstract string GetSceneName(TScene scene);
        protected abstract void SetSceneName(TScene scene, string name);
        protected abstract TScene MakeScene(string name);
        public abstract TScene GetScene(string name);
        public abstract bool HasScene(string name);
        protected abstract bool CoreRemoveScene(string name);
        protected abstract void CoreStore(TScene scene);

        public TScene CreateScene(string name)
        {
            var scene = MakeScene(name);
            Store(scene);
            var e = new ActionSceneResultEventArgs(name, SceneActions.Created);
            RaiseCreatedScene(e);
            return scene;
        }


        public bool RemoveScene(string name)
        {
            if (CoreRemoveScene(name))
            {
                var e = new ActionSceneResultEventArgs(name, SceneActions.Removed);
                RaiseRemovedScene(e);
                return true;
            }
            return false;
        }

        public void Rename(string oldName, string newName)
        {
            var sc = GetScene(oldName);
            if (sc == null)
            {
                throw new InvalidOperationException($"Can'find scene {oldName}");
            }
            SetSceneName(sc, newName);
            var e = new TwoSceneResultEventArgs(oldName, newName);
            RaiseRenamedScene(e);
        }

        public void Store(TScene scene)
        {
            CoreStore(scene);
            RaiseStoredScene(new StoredSceneEventArgs<TScene, TDesignObject>(scene));
        }
    }
}
