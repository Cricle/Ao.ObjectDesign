using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Session.Controllers
{
    public abstract class WpfSceneController<TSetting> : WpfLazyMapSceneController<TSetting>
    {
        public WpfSceneController(IDesignPackage<UIElement,TSetting,IWithSourceBindingScope> designMap,
            IObservableDesignScene<TSetting> scene)
            : base(designMap)
        {
            this.scene = scene ?? throw new ArgumentNullException(nameof(scene));
        }

        private readonly IObservableDesignScene<TSetting> scene;

        public override IObservableDesignScene<TSetting> GetScene()
        {
            return scene;
        }
        protected override bool CanBuildNext(IDesignPair<UIElement, TSetting> pair)
        {
            return pair.DesigningObject is IObservableDesignScene<TSetting>;
        }
        protected override IDesignSceneController<UIElement, TSetting> CreateController(IDesignPair<UIElement, TSetting> pair)
        {
            var sc = (IObservableDesignScene<TSetting>)pair.DesigningObject;
            if (pair.UI is Panel p)
            {
                var eles = p.Children;
                var controller = new WpfSceneItemsSceneController<TSetting>(DesignPackage, eles, sc)
                {
                    LazyBinding = LazyBinding,
                    IgnoreBinding = IgnoreBinding
                };
                controller.Initialize();
                return controller;
            }
            else if (pair.UI is ItemsControl ic)
            {
                var obs = new SilentObservableCollection<UIElement>(ic.Items.Cast<UIElement>());
                var controller = new WpfSceneObservableSceneController<TSetting>(DesignPackage, obs, sc)
                {
                    designPair = pair,
                    LazyBinding = LazyBinding,
                    IgnoreBinding = IgnoreBinding
                };
                controller.Initialize();
                return controller;
            }
            else
            {
                throw new NotSupportedException(pair.UI?.ToString());
            }
        }
        protected override void OnDispose()
        {
            base.OnDispose();
            IEnumerable<IDesignSceneController<UIElement, TSetting>> controller = DesignObjectUnitNextMap.Values;
            while (controller.Any())
            {
                foreach (var item in controller)
                {
                    if (item is WpfLazyMapSceneController<TSetting> lmsc)
                    {
                        lmsc.BindingTaskMap.Clear();
                    }
                    item.Dispose();
                }
                controller = controller.SelectMany(x => x.DesignObjectUnitNextMap.Values);
            }
        }
    }
}
