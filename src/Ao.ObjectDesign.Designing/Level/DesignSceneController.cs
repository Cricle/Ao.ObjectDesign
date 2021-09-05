using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Level
{
    public abstract class DesignSceneController<TUI, TDesignObject> : IDisposable, IDesignSceneController<TUI, TDesignObject>
    {
        protected DesignSceneController()
        {
            designUnitMap = new Dictionary<TUI, IDesignPair<TUI, TDesignObject>>();
            designUnits = new SilentObservableCollection<IDesignPair<TUI, TDesignObject>>();
            designObjectUnitMap = new Dictionary<TDesignObject, IDesignPair<TUI, TDesignObject>>();

            nexts = new SceneMap<TUI, TDesignObject>();
            designUnitNextMap = new Dictionary<TUI, DesignSceneController<TUI, TDesignObject>>();
            designObjectUnitNextMap = new Dictionary<TDesignObject, DesignSceneController<TUI, TDesignObject>>();
        }

        private bool isInitialized;
        private IObservableDeisgnScene<TDesignObject> scene;

        private readonly SilentObservableCollection<IDesignPair<TUI, TDesignObject>> designUnits;

        private readonly Dictionary<TUI, IDesignPair<TUI, TDesignObject>> designUnitMap;
        private readonly Dictionary<TDesignObject, IDesignPair<TUI, TDesignObject>> designObjectUnitMap;

        private readonly SceneMap<TUI, TDesignObject> nexts;
        private readonly Dictionary<TUI, DesignSceneController<TUI, TDesignObject>> designUnitNextMap;
        private readonly Dictionary<TDesignObject, DesignSceneController<TUI, TDesignObject>> designObjectUnitNextMap;

        public IObservableDeisgnScene<TDesignObject> Scene => scene;

        public bool IsInitialized => isInitialized;

        public IReadOnlyDictionary<TUI, IDesignPair<TUI, TDesignObject>> DesignUnitMap => designUnitMap;

        public IReadOnlyDictionary<TDesignObject, IDesignPair<TUI, TDesignObject>> DesignObjectUnitMap => designObjectUnitMap;

        public IReadOnlyList<IDesignPair<TUI, TDesignObject>> DesignUnits => designUnits;

        public IReadOnlySceneMap<TUI, TDesignObject> Nexts => nexts;

        public IReadOnlyDictionary<TUI, DesignSceneController<TUI, TDesignObject>> DesignUnitNextMap => designUnitNextMap;

        public IReadOnlyDictionary<TDesignObject, DesignSceneController<TUI, TDesignObject>> DesignObjectUnitNextMap => designObjectUnitNextMap;

        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }
            scene = GetScene();
            AddUnits(scene.DesigningObjects);
            scene.DesigningObjects.CollectionChanged += OnDesigningObjectsCollectionChanged;
            OnInitialize();
            isInitialized = true;
        }
        protected virtual void OnInitialize()
        {

        }

        private void OnDesigningObjectsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                AddUnits(e.NewItems.OfType<TDesignObject>());
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                RemoveUnits(e.OldItems.OfType<TDesignObject>());
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var item in designUnits)
                {
                    RemoveUIElement(item);
                }
                designUnits.Clear();
                designUnitMap.Clear();
                designObjectUnitMap.Clear();
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                designUnits.Move(e.OldStartingIndex, e.NewStartingIndex);
            }
        }
        protected virtual void RemoveUnits(IEnumerable<TDesignObject> designingObjects)
        {
            var designObjs = new HashSet<TDesignObject>(designingObjects);
            var units = designUnits.Where(x => designObjs.Contains(x.DesigningObject)).ToList();
            foreach (var item in units)
            {
                designUnits.Remove(item);
                designUnitMap.Remove(item.UI);
                designObjectUnitMap.Remove(item.DesigningObject);
                RemoveUIElement(item);

                if (nexts.TryGetValue(item,out var controller))
                {
                    nexts.Remove(item);
                    designUnitNextMap.Remove(item.UI);
                    designObjectUnitNextMap.Remove(item.DesigningObject);
                    controller.Dispose();
                }
            }
        }
        protected virtual void AddUnits(IEnumerable<TDesignObject> designingObjects)
        {
            foreach (var item in designingObjects)
            {
                var ui = CreateUI(item);
                var unit = CreatetDesignUnit(ui, item);
                designUnits.Add(unit);
                designUnitMap.Add(ui, unit);
                designObjectUnitMap.Add(item, unit);
                AddUIElement(unit);

                if (CanBuildNext(unit))
                {
                    var controller = CreateController(unit);
                    controller.Initialize();
                    nexts.Add(unit,controller);
                    designUnitNextMap.Add(ui, controller);
                    designObjectUnitNextMap.Add(item, controller);
                }
            }
        }

        protected virtual DesignSceneController<TUI, TDesignObject> CreateController(IDesignPair<TUI, TDesignObject> pair)
        {
            throw new NotImplementedException();
        }
        protected virtual bool CanBuildNext(IDesignPair<TUI,TDesignObject> pair)
        {
            return false;
        }

        public abstract IObservableDeisgnScene<TDesignObject> GetScene();

        protected abstract void AddUIElement(IDesignPair<TUI, TDesignObject> unit);

        protected abstract void RemoveUIElement(IDesignPair<TUI, TDesignObject> unit);

        protected virtual IDesignPair<TUI, TDesignObject> CreatetDesignUnit(TUI ui, TDesignObject @object)
        {
            return new DesignPair<TUI, TDesignObject>(ui, @object);
        }

        protected abstract TUI CreateUI(TDesignObject designingObject);

        public void Dispose()
        {
            isInitialized = false;
            Scene.DesigningObjects.CollectionChanged -= OnDesigningObjectsCollectionChanged;
            foreach (var item in nexts.Values)
            {
                item.Dispose();
            }
            nexts.Clear();
            designObjectUnitNextMap.Clear();
            designObjectUnitMap.Clear();
            designUnitNextMap.Clear();
            designUnits.Clear();
            designUnitMap.Clear();
            OnDispose();
        }
        protected virtual void OnDispose()
        {

        }
    }
}
