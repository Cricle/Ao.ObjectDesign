using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;

namespace Ao.ObjectDesign.Session.Controllers
{
    public class WpfItemsSceneController<TSetting> : WpfSceneController<TSetting>
    {
        public WpfItemsSceneController(IDesignPackage<TSetting> designMap,
            IList uiElements,
            IObservableDesignScene<TSetting> scene)
            : base(designMap, scene)
        {
            UIElements = uiElements;
        }

        public IList UIElements { get; }

        private void OnDesigningObjectsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                var oldIdx = e.OldStartingIndex;
                var newIdx = e.NewStartingIndex;
                var val = UIElements[oldIdx];
                UIElements.RemoveAt(oldIdx);
                UIElements.Insert(newIdx, val);
            }
        }
        protected override void OnInitialize()
        {
            Scene.DesigningObjects.CollectionChanged += OnDesigningObjectsCollectionChanged;
        }
        protected override void OnDispose()
        {
            base.OnDispose();
            Scene.DesigningObjects.CollectionChanged -= OnDesigningObjectsCollectionChanged;
        }

        protected override void RemoveFromContainer(IDesignPair<UIElement, TSetting> unit)
        {
            UIElements.Remove(unit.UI);
        }
        protected override void AddToContainer(IDesignPair<UIElement, TSetting> unit)
        {
            UIElements.Add(unit.UI);
        }
    }
}
