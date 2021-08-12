using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class MemoryWorkplace<TScene, TDesignObject> : Workplace<TScene, TDesignObject>
        where TScene : IDeisgnScene<TDesignObject>
    {
        private readonly SilentObservableCollection<TScene> scenes;

        public MemoryWorkplace(IEnumerable<TScene> scenes)
        {
            if (scenes is null)
            {
                throw new ArgumentNullException(nameof(scenes));
            }

            this.scenes = new SilentObservableCollection<TScene>(scenes);
        }
        public MemoryWorkplace()
        {
            scenes = new SilentObservableCollection<TScene>();
        }

        public IReadOnlyList<TScene> Scenes => scenes;

        public override IReadOnlyList<string> SceneNames => scenes.Select(GetSceneName).ToList();

        protected override void CoreAddScene(TScene scene, string name)
        {
            scenes.Add(scene);
        }
        protected override bool CoreRemoveScene(string name)
        {
            var scene = GetScene(name);
            if (scene == null)
            {
                return false;
            }
            return scenes.Remove(scene);
        }
        protected override void CoreStore(TScene scene)
        {
            if (scene != null)
            {
                var index = scenes.IndexOf(scene);
                scenes[index] = scene;
            }
            else
            {
                scenes.Add(scene);
            }
        }
        public override TScene GetScene(string name)
        {
            return scenes.FirstOrDefault(x=>GetSceneName(x)==name);
        }

        public override bool HasScene(string name)
        {
            return scenes.Any(x => GetSceneName(x) == name);
        }
        protected override TScene CoreCopyScene(string sourceName, string destName)
        {
            var val = EnsureGetScene(sourceName);
            var copied = (TScene)ReflectionHelper.Clone(val.GetType(), val, DesigningHelpers.KnowDesigningTypes);
            SetSceneName(copied, destName);
            CoreAddScene(copied, destName);
            return copied;
        }
    }
}
