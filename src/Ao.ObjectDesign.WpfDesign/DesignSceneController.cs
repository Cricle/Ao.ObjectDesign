using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class DesignSceneController<TDesignObject> : IDisposable
    {
        protected DesignSceneController()
        {
            designUnits = new SilentObservableCollection<IDesignUnit<TDesignObject>>();
            designUnitMap = new Dictionary<UIElement, IDesignUnit<TDesignObject>>();
        }

        private bool isInitialized;
        private IDeisgnScene<TDesignObject> scene;

        private readonly SilentObservableCollection<IDesignUnit<TDesignObject>> designUnits;

        private readonly Dictionary<UIElement, IDesignUnit<TDesignObject>> designUnitMap;

        public IDeisgnScene<TDesignObject> Scene => scene;

        public bool IsInitialized => isInitialized;

        public IReadOnlyDictionary<UIElement, IDesignUnit<TDesignObject>> DesignUnitMap => designUnitMap;

        public IReadOnlyList<IDesignUnit<TDesignObject>> DesignUnits => designUnits;

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
        }
        protected virtual void RemoveUnits(IEnumerable<TDesignObject> designingObjects)
        {
            var designObjs = new HashSet<TDesignObject>(designingObjects);
            var units = designUnits.Where(x => designObjs.Contains(x.DesigningObject)).ToList();
            foreach (var item in units)
            {
                designUnits.Remove(item);
                designUnitMap.Remove(item.UI);
                RemoveUIElement(item);
            }
        }
        protected virtual void AddUnits(IEnumerable<TDesignObject> designingObjects)
        {
            foreach (var item in designingObjects)
            {
                var ui = CreateUI(item);
                var unit = CreatetDesignUnit(ui, item);
                unit.Build();
                unit.Bind();
                designUnits.Add(unit);
                designUnitMap.Add(ui, unit);
                AddUIElement(unit);
            }
        }

        public abstract IDeisgnScene<TDesignObject> GetScene();

        protected abstract void AddUIElement(IDesignUnit<TDesignObject> unit);

        protected abstract void RemoveUIElement(IDesignUnit<TDesignObject> unit);

        protected abstract IDesignUnit<TDesignObject> CreatetDesignUnit(UIElement ui, TDesignObject @object);

        protected abstract UIElement CreateUI(TDesignObject designingObject);

        public void Dispose()
        {
            Scene.DesigningObjects.CollectionChanged -= OnDesigningObjectsCollectionChanged;
            OnDispose();
        }
        protected virtual void OnDispose()
        {

        }
    }
}
