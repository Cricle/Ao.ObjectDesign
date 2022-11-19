using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using Ao.ObjectDesign.WpfDesign;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Session.Wpf.Controllers
{
    public class WpfSceneItemsSceneController<TSetting> : WpfSceneController<TSetting>
    {
        public WpfSceneItemsSceneController(IDesignPackage<UIElement, TSetting, IWithSourceBindingScope> designMap,
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
        protected override LazyBindingBox<UIElement, TSetting, IWithSourceBindingScope, BindingExpressionBase> CreateBindingBox(IDesignPair<UIElement, TSetting> unit, IEnumerable<IWithSourceBindingScope> scopes)
        {
            return new WpfLazyBindingBox<TSetting>(unit, scopes.ToList());
        }
    }
}
