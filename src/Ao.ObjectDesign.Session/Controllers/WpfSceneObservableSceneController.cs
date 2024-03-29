﻿using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.Session.Controllers
{
    public class WpfSceneObservableSceneController<TSetting> : WpfSceneController<TSetting>
    {
        public WpfSceneObservableSceneController(IDesignPackage<UIElement, TSetting, IWithSourceBindingScope> designMap,
            SilentObservableCollection<UIElement> items,
            IObservableDesignScene<TSetting> scene)
            : base(designMap, scene)
        {
            Items = items;
        }
        public WpfSceneObservableSceneController(IDesignPackage<UIElement, TSetting, IWithSourceBindingScope> designMap,
            IObservableDesignScene<TSetting> scene)
            : this(designMap, new SilentObservableCollection<UIElement>(), scene)
        {
        }
        internal IDesignPair<UIElement, TSetting> designPair;

        public SilentObservableCollection<UIElement> Items { get; }

        private void OnDesigningObjectsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                Items.Move(e.OldStartingIndex, e.NewStartingIndex);
            }
        }

        protected override void OnInitialize()
        {
            var ic = (ItemsControl)designPair.UI;
            ic.ItemsSource = Items;
            Scene.DesigningObjects.CollectionChanged += OnDesigningObjectsCollectionChanged;
        }
        protected override void OnDispose()
        {
            base.OnDispose();
            var ic = (ItemsControl)designPair.UI;
            ic.ItemsSource = null;
            Scene.DesigningObjects.CollectionChanged -= OnDesigningObjectsCollectionChanged;
            Items.Clear();
        }

        protected override void AddToContainer(IDesignPair<UIElement, TSetting> unit)
        {
            Items.Add(unit.UI);
        }

        protected override void RemoveFromContainer(IDesignPair<UIElement, TSetting> unit)
        {
            Items.Remove(unit.UI);
        }

        protected override LazyBindingBox<UIElement, TSetting, IWithSourceBindingScope, BindingExpressionBase> CreateBindingBox(IDesignPair<UIElement, TSetting> unit, IEnumerable<IWithSourceBindingScope> scopes)
        {
            return new WpfLazyBindingBox<TSetting>(unit, scopes.ToList());
        }
    }
}
