using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Level
{
    public abstract class SceneManager<TUI, TScene, TDesignObject>
        where TScene : IDesignScene<TDesignObject>
    {
        private TScene currentScene;
        private IDesignSceneController<TUI, TDesignObject> currentSceneController;

        public virtual IDesignSceneController<TUI, TDesignObject> CurrentSceneController => currentSceneController;

        public virtual TScene CurrentScene
        {
            get => currentScene;
            set
            {
                var old = currentScene;
                if (!EqualityComparer<TScene>.Default.Equals(old, value))
                {
                    OnCurrentSceneChanging(old, value);
                    currentScene = value;
                    var e = new CurrentSceneChangedEventArgs<TDesignObject>(old, value);
                    OnCurrentSceneChanged(e);
                    CurrentSceneChanged?.Invoke(this, e);
                    BuildController();
                }
            }
        }

        public event EventHandler<CurrentSceneChangedEventArgs<TDesignObject>> CurrentSceneChanged;
        public event EventHandler<CurrentSceneControllerChangedEventArgs<TUI, TDesignObject>> CurrentSceneControllerChanged;

        public abstract DesignSceneController<TUI, TDesignObject> CreateSceneController(TScene scene);

        private void BuildController()
        {
            var scene = currentScene;
            if (scene != null)
            {
                var old = currentSceneController;
                var controller = CreateSceneController(scene);
                currentSceneController = controller;
                var e = new CurrentSceneControllerChangedEventArgs<TUI, TDesignObject>(old, controller);
                OnCurrentSceneControllerChanged(e);
                controller.Initialize();
                CurrentSceneControllerChanged?.Invoke(this, e);
            }
        }
        protected virtual void OnCurrentSceneChanged(CurrentSceneChangedEventArgs<TDesignObject> e)
        {

        }
        protected virtual void OnCurrentSceneControllerChanged(CurrentSceneControllerChangedEventArgs<TUI, TDesignObject> e)
        {
            e.Old?.Dispose();
            e.New?.Initialize();
        }

        public IEnumerable<IDesignPair<TUI, TDesignObject>> EnumerableHitTest(Func<TDesignObject, IRect> boundGetter, IVector point)
        {
            if (boundGetter is null)
            {
                throw new ArgumentNullException(nameof(boundGetter));
            }

            IDesignSceneController<TUI, TDesignObject> controller = CurrentSceneController;
            if (controller is null)
            {
                return Enumerable.Empty<IDesignPair<TUI, TDesignObject>>();
            }
            return controller.Lookup(x => boundGetter(x))
                .Where(x => x.ActualBounds.Contains(point.X, point.Y))
                .Select(x => x.Pair);
        }
        private IEnumerable<IDesignPair<TUI, TDesignObject>> HitTestCore(IEnumerable<IDesignSceneController<TUI, TDesignObject>> controllers,
            Func<TDesignObject, IRect> boundGetter,
            IVector point,
            IVector offset)
        {
            if (boundGetter is null)
            {
                throw new ArgumentNullException(nameof(boundGetter));
            }

            foreach (var item in controllers)
            {
                var sc = (TDesignObject)item.Scene;
                var v = boundGetter(sc);
                var voffset = new DefaultVector(v.Left, v.Top);
                foreach (var n in HitTestCore(item.Nexts.Values, boundGetter, point, voffset))
                {
                    yield return n;
                }
                var units = item.DesignUnits;
                var count = units.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    IDesignPair<TUI, TDesignObject> unit = units[i];
                    var size = boundGetter(unit.DesigningObject);
                    if (size != null)
                    {
                        var bounds = new DefaultRect(size.Left + offset.X, size.Top + offset.Y, size.Right - size.Left, size.Bottom - size.Top);
                        if (bounds.Contains(point.X, point.Y))
                        {
                            yield return unit;
                        }
                    }
                }
            }
        }
        public IDesignPair<TUI, TDesignObject> HitTest(Func<TDesignObject, IRect> boundGetter,
            IVector point)
        {
            if (boundGetter is null)
            {
                throw new ArgumentNullException(nameof(boundGetter));
            }

            if (CurrentSceneController is null)
            {
                return null;
            }
            return EnumerableHitTest(boundGetter, point).FirstOrDefault();
        }


        public IEnumerable<IDesignPair<TUI, TDesignObject>> EnumerableHitTest(IVector point)
        {
            return EnumerableHitTest(GetBounds, point);
        }
        public IDesignPair<TUI, TDesignObject> HitTest(IVector point)
        {
            return HitTest(GetBounds, point);
        }
        protected virtual IRect GetBounds(TDesignObject setting)
        {
            if (setting is IBoundsProvider provider)
            {
                return provider.GetBounds();
            }
            return null;
        }
        protected virtual void OnCurrentSceneChanging(TScene old, TScene @new)
        {
        }
    }
}
